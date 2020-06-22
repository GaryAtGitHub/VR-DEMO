using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class DoorHighlight : MonoBehaviour
{
    public EventDefine ActiveStep;
    public Color m_color;
    public VRTK_OutlineObjectCopyHighlighter m_HighLighter;
    public CableAndDrillZoneManager TriggersManager;

    private bool IsTrigger = false;

    private void Start()
    {
        m_HighLighter = m_HighLighter ? GetComponent<VRTK_OutlineObjectCopyHighlighter>() : m_HighLighter;

        if (m_HighLighter != null)
        {
            m_HighLighter.Initialise();
        }      
        EventCenter.AddListener(ActiveStep, HighLight);
        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(ActiveStep, HighLight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTrigger) return;
        if (other.tag == "Hand")
        {
            UnHighLight();
        }
        IsTrigger = true;
        if (TriggersManager != null)
        {
            TriggersManager.TriggerUpdate(gameObject);
        }
    }

    private void HighLight()
    {
        m_HighLighter.Highlight(m_color);
    }

    private void UnHighLight()
    {
        m_HighLighter.Unhighlight();
    }
}
