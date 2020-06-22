using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class DrillZoneController : MonoBehaviour
{
    public Transform m_DrillFrontHoleParent;
    public Transform m_DrillRearHoleParent;
    public Color m_HighLightColor;

    public bool m_IsActiveStep;

    private VRTK_OutlineObjectCopyHighlighter highLighter;

    public void ForceUnHighLight()
    {
        highLighter.Unhighlight();
    }

    private void Start()
    {
        highLighter = GetComponent<VRTK_OutlineObjectCopyHighlighter>();
        if (highLighter != null)
        {
            highLighter.Initialise();
        }
        EventCenter.AddListener(EventDefine.WeildDrill, HighLight);
        EventCenter.AddListener(EventDefine.UnWeildDrill, UnHighLight);        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.WeildDrill, HighLight);
        EventCenter.RemoveListener(EventDefine.UnWeildDrill, UnHighLight);
    }

    private void HighLight()
    {
        if (!m_IsActiveStep) return;
        highLighter.Highlight(m_HighLightColor);
    }

    private void UnHighLight()
    {
        if (!m_IsActiveStep) return;
        highLighter.Unhighlight();
    }

}
