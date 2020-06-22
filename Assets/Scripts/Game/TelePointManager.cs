using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TelePointManager : MonoBehaviour
{
    /// <summary>
    /// The Steps that resume the TelePoint Manager
    /// </summary>
    public EventDefine[] ResumeFromeSteps;
    public GameObject Indicator;

    private Transform m_TelePoint;
    private VRTK_DestinationPoint m_DestinationPoint;
    private int nextIndex = 0;
    private ItemStepController_Basic StepController;
    private bool m_IsPause;

    private void Start()
    {
        NextTelePoint();
        foreach (EventDefine step in ResumeFromeSteps)
        {
            EventCenter.AddListener(step, Resume);
        }
    }

    private void OnDestroy()
    {
        if (m_DestinationPoint != null)
        {
            m_DestinationPoint.DestinationPointDisabled -= M_DestinationPoint_DestinationPointDisabled;
        }
        foreach (EventDefine step in ResumeFromeSteps)
        {
            EventCenter.RemoveListener(step, Resume);
        }
    }

    public void NextTelePoint()
    {
        if (nextIndex < transform.childCount)
        {       
            if (nextIndex != 0)
            {
                m_DestinationPoint.DestinationPointDisabled -= M_DestinationPoint_DestinationPointDisabled;
                Transform PreviousTelePoint = m_TelePoint;
                StartCoroutine(DisableTelePoint(PreviousTelePoint));
            }
            m_TelePoint = transform.GetChild(nextIndex++);
            m_TelePoint.gameObject.SetActive(true);

            StepController = m_TelePoint.GetComponent<ItemStepController_Basic>();

            if (StepController != null)
            {
                m_IsPause = true;
            }


            if (Indicator != null)
            {
                Indicator.gameObject.SetActive(true);
                Indicator.GetComponent<My3DArrow>().follow = m_TelePoint.gameObject;
            }

            m_DestinationPoint = m_TelePoint.GetComponent<VRTK_DestinationPoint>();
            m_DestinationPoint.DestinationPointDisabled += M_DestinationPoint_DestinationPointDisabled;
        }
        else if (nextIndex == transform.childCount) //disable the indicator gameobject for the last telepoint
        {
            Indicator.gameObject.SetActive(false);
        }
    }

    private void M_DestinationPoint_DestinationPointDisabled(object sender)
    {
        if (!m_IsPause)
        {
            NextTelePoint();
        }
        else
        {
            UpdateSceneManager();
            Indicator.gameObject.SetActive(false);
        }        
    }

    private void UpdateSceneManager()
    {
        switch (StepController.Steps[0])//The Steps[0] contains the index of the next step
        {
            case EventDefine.Step0:
                break;
            case EventDefine.Step1:
                Lv1Manager.Instance.m_IsEnterStep1 = true;
                break;
            case EventDefine.Step3:
                Lv1Manager.Instance.m_IsEnterStep3 = true;
                break;
            case EventDefine.Step5:
                Lv1Manager.Instance.m_IsEnterStep5 = true;
                break;
            case EventDefine.Step7:
                Lv1Manager.Instance.m_IsEnterStep7 = true;
                break;
            default:
                break;
        }
    }

    private void Resume()
    {
        m_IsPause = false;
        NextTelePoint();
    }

    IEnumerator DisableTelePoint(Transform PreviousTelePoint)
    {
        yield return new WaitForEndOfFrame();
        PreviousTelePoint.gameObject.SetActive(false);
    }
}

