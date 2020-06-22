using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1Manager : MonoBehaviour
{
    public bool m_IsEnterStep1;
    public bool m_IsEntgerStep2;
    public bool m_IsEnterStep3;
    public bool m_IsEnterStep4;
    public bool m_IsEnterStep5;
    public bool m_IsEnterStep6;
    public bool m_IsEnterStep7;

    public static Lv1Manager Instance
    {
        get; private set;
    }

    private bool m_IsLastStep = false;
    public int m_CurrentStep = 0;//change to public for testing
    private int m_NextStep = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Manage the operation for each step of current level
        switch (m_CurrentStep)
        {
            case 0:
                if (m_IsEnterStep1)
                {
                    NextStep();
                }
                break;
            case 1:
                if (m_IsEntgerStep2)
                {
                    NextStep();
                }
                break;
            case 2:
                if (m_IsEnterStep3)
                {
                    NextStep();
                }
                break;
            case 3:
                if (m_IsEnterStep4)
                {
                    NextStep();
                }
                break;
            case 4:
                if (m_IsEnterStep5)
                {
                    NextStep();
                }
                break;
            case 5:
                if (m_IsEnterStep6)
                {
                    NextStep();
                }
                break;
            case 6:
                if (m_IsEnterStep7)
                {
                    NextStep();
                }
                break;
            case 7:
                break;
            default:
                throw new Exception(string.Format("Step{0} not defined", m_CurrentStep));
                break;
        }
    }

    /// <summary>
    /// Brocast Next step
    /// </summary>
    /// <param name="forceStep"></param> specifice the index of next step. If no index is provided, the next step of current will be brocasted.
    private void NextStep(int forceStep = -1)
    {
        if (forceStep != -1)
        {
            m_NextStep = forceStep;
        }
        EventCenter.Broadcast((EventDefine)Enum.Parse(typeof(EventDefine), "Step" + m_NextStep.ToString()));
        Debug.Log(string.Format("Setp{0} broadcasted", m_NextStep));
        m_CurrentStep = m_NextStep;
        m_NextStep++;
    }
}
