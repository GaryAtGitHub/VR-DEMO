using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandAnimationController : MonoBehaviour
{
    public VRTK_InteractGrab m_InteractGrab;

    private Animator m_Animator;
    private bool m_IsGrabbing = false;

    private void OnEnable()
    {
        m_InteractGrab.ControllerGrabInteractableObject += M_InteractGrab_ControllerGrabInteractableObject;
        m_InteractGrab.ControllerUngrabInteractableObject += M_InteractGrab_ControllerUngrabInteractableObject;
        m_InteractGrab.GrabButtonPressed += M_InteractGrab_GrabButtonPressed;
        m_InteractGrab.GrabButtonReleased += M_InteractGrab_GrabButtonReleased;
    }

    private void OnDisable()
    {
        m_InteractGrab.ControllerGrabInteractableObject -= M_InteractGrab_ControllerGrabInteractableObject;
        m_InteractGrab.ControllerUngrabInteractableObject -= M_InteractGrab_ControllerUngrabInteractableObject;
        m_InteractGrab.GrabButtonPressed -= M_InteractGrab_GrabButtonPressed;
        m_InteractGrab.GrabButtonReleased -= M_InteractGrab_GrabButtonReleased;
    }

    private void M_InteractGrab_GrabButtonReleased(object sender, ControllerInteractionEventArgs e)
    {
        if (!m_IsGrabbing)
        {
            m_Animator.SetBool("Catch", false);
        }        
    }

    private void M_InteractGrab_GrabButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!m_IsGrabbing)
        {
            m_Animator.SetBool("Catch", true);
        }         
    }

    private void M_InteractGrab_ControllerUngrabInteractableObject(object sender, ObjectInteractEventArgs e)
    {
        m_Animator.SetBool("Catch", false);
        m_IsGrabbing = false;
    }

    private void M_InteractGrab_ControllerGrabInteractableObject(object sender, ObjectInteractEventArgs e)
    {
        m_Animator.SetBool("Catch", true);
        m_IsGrabbing = true;
    }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        if (m_Animator != null)
        {
            Debug.Log("Animator Got!");
        }
    }

}
