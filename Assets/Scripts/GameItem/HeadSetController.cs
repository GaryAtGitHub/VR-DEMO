using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class HeadSetController : MonoBehaviour
{
    public GameObject go_Arrow;
    private bool m_isActivated = false;
    private VRTK_BaseHighlighter m_Highter;
    private VRTK_InteractableObject m_VRTKIO;

    private void Start()
    {
        m_Highter = GetComponent<VRTK_BaseHighlighter>();
        m_VRTKIO = GetComponent<VRTK_InteractableObject>();
        m_VRTKIO.InteractableObjectGrabbed += M_VRTKIO_InteractableObjectGrabbed;
        m_VRTKIO.InteractableObjectUngrabbed += M_VRTKIO_InteractableObjectUngrabbed;
        m_VRTKIO.InteractableObjectTouched += M_VRTKIO_InteractableObjectTouched;
    }

    private void M_VRTKIO_InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        if (!m_isActivated) return;        
        Destroy(go_Arrow);
        m_VRTKIO.InteractableObjectTouched -= M_VRTKIO_InteractableObjectTouched;
    }

    private void M_VRTKIO_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (!m_isActivated) return;
        m_Highter.Highlight(Color.yellow);
    }

    private void M_VRTKIO_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (!m_isActivated) return;
        m_Highter.Unhighlight();
    }

    private void OnDestroy()
    {
        m_VRTKIO.InteractableObjectGrabbed -= M_VRTKIO_InteractableObjectGrabbed;
        m_VRTKIO.InteractableObjectUngrabbed -= M_VRTKIO_InteractableObjectUngrabbed;
    }

    public void Activate()
    {
        if (!m_isActivated)
        {
            m_isActivated = true;
            m_Highter.Initialise();
            m_Highter.Highlight(Color.yellow);
        }
    }


}
