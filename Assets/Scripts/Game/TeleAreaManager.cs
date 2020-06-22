using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleAreaManager : MonoBehaviour
{
    public VRTK_Pointer m_Pointer;
    
    private void Start()
    {
        if (m_Pointer == null)
        {
            VRTK_Pointer[] Pointers = FindObjectsOfType<VRTK_Pointer>();
            foreach (var pointer in Pointers)
            {
                if (pointer.enableTeleport == true)
                {
                    m_Pointer = pointer;
                    break;
                }
            }
        }
        m_Pointer.ActivationButtonPressed += M_Pointer_ActivationButtonPressed;
        m_Pointer.ActivationButtonReleased += M_Pointer_ActivationButtonReleased;
        SetChildActive(false);
    }

    private void OnDestroy()
    {
        m_Pointer.ActivationButtonPressed -= M_Pointer_ActivationButtonPressed;
        m_Pointer.ActivationButtonReleased -= M_Pointer_ActivationButtonReleased;
    }

    private void M_Pointer_ActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        SetChildActive(true);
    }

    private void M_Pointer_ActivationButtonReleased(object sender, ControllerInteractionEventArgs e)
    {
        SetChildActive(false);
    }

    private void SetChildActive(bool isActive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
