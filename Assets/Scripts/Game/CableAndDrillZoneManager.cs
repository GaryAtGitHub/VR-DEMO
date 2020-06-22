using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableAndDrillZoneManager : MonoBehaviour
{
    public GameObject[] m_TriggerObjects;

    private DrillZoneController drillZoneController;
    private int TriggeredNum = 0;

    private void Start()
    {
        drillZoneController = GetComponentInChildren<DrillZoneController>();
    }

    public void TriggerUpdate(GameObject sender)
    {
        if (Array.Exists<GameObject>(m_TriggerObjects,  x => x == sender))
        TriggeredNum++;
        Debug.Log(TriggeredNum.ToString());
        CheckTriggers();
    }

    public void CheckTriggers()
    {
        if (TriggeredNum == 2)
        {
            if (drillZoneController != null)
            {
                drillZoneController.m_IsActiveStep = true;
            }
        }
        if (TriggeredNum == m_TriggerObjects.Length)
        {
            if (drillZoneController != null)
            {
                drillZoneController.m_IsActiveStep = false;
                drillZoneController.ForceUnHighLight();
            }
            Lv1Manager.Instance.m_IsEnterStep4 = true;
            Debug.Log("IsEngterSet Sent");
        }
    }
}
