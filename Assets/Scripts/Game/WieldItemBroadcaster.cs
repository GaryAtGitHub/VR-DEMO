using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WieldItemBroadcaster : MonoBehaviour
{
    public VRTK_InteractGrab m_InteractGrab;

    private void Start()
    {
        m_InteractGrab = (m_InteractGrab != null) ? m_InteractGrab : GetComponent<VRTK_InteractGrab>();
        m_InteractGrab.ControllerGrabInteractableObject += M_InteractGrab_ControllerGrabInteractableObject;
        m_InteractGrab.ControllerUngrabInteractableObject += M_InteractGrab_ControllerUngrabInteractableObject;
    }

    private void OnDestroy()
    {
        m_InteractGrab.ControllerGrabInteractableObject -= M_InteractGrab_ControllerGrabInteractableObject;
        m_InteractGrab.ControllerUngrabInteractableObject -= M_InteractGrab_ControllerUngrabInteractableObject;
    }

    private void M_InteractGrab_ControllerUngrabInteractableObject(object sender, ObjectInteractEventArgs e)
    {
        if (e.target.CompareTag("Drill"))
        {
            EventCenter.Broadcast(EventDefine.UnWeildDrill);
        }
    }

    private void M_InteractGrab_ControllerGrabInteractableObject(object sender, ObjectInteractEventArgs e)
    {
        if (e.target.CompareTag("Drill"))
        {
            EventCenter.Broadcast(EventDefine.WeildDrill);
        }
    }
}
