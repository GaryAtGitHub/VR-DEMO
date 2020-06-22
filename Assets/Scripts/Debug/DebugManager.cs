using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public EventDefine Step;
    public void EventBroadcast()
    {
        
        EventCenter.Broadcast(Step);
        Lv1Manager.Instance.m_CurrentStep = (int)Step%10;
    }
}
