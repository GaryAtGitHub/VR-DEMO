using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Basic : MonoBehaviour
{
    protected DisplayManger DisplayManager;
    protected Button button;

    protected virtual void Start()
    {
        DisplayManager = GetComponentInParent<WelcomPage>().m_DisplayManger;
    }
}
