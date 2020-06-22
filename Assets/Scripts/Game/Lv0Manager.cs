using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Lv0Manager : MonoBehaviour
{
    //flag for step
    public string[] LevelName;
    public bool m_IsLevelSelected = false;
    //

    public static Lv0Manager Instance
    {
        get; private set;
    }

    private bool m_IsLoad = false;
    private bool m_IsLastStep = false;
    private int m_CurrentStep = 0;
    private int m_NextStep = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        EventCenter.AddListener<int>(EventDefine.LoadLevel, LoadLevel);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.LoadLevel, LoadLevel);
    }

    private void Update()
    {
        //Manage the operation for each step of current level
        switch (m_CurrentStep)
        {
            case 0:
                if (m_IsLevelSelected)
                {
                    NextStep();
                }
                break;
            case 1:
                if (!m_IsLastStep)
                {
                    m_IsLastStep = true;
                }
                break;
            default:
                throw new Exception(string.Format("Step{0} not defined", m_CurrentStep));
                break;
        }
    }

    private void NextStep(int forceStep = 0)
    {
        if (forceStep != 0)
        {
            m_NextStep = forceStep;
        }
        EventCenter.Broadcast((EventDefine)Enum.Parse(typeof(EventDefine), "Step" + m_NextStep.ToString()));
        m_CurrentStep = m_NextStep;
        m_NextStep++;
    }

    private void LoadLevel(int index)
    {
        if (m_IsLoad) return;
        if (!m_IsLastStep) return;
        m_IsLoad = true;
        Debug.Log(string.Format("Level{0} loaded", index));       
        SteamVR_LoadLevel.Begin(LevelName[index-1]);
    }
}
