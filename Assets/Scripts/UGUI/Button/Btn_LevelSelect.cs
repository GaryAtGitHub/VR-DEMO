using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_LevelSelect : MonoBehaviour
{
    private bool m_isPressed = false;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            if (!m_isPressed)
            {
                Lv0Manager.Instance.m_IsLevelSelected = true;
                m_isPressed = true;
            }          
        });
    }
}
