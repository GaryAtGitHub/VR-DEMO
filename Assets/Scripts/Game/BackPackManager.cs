using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Belt,
    Drill,
    Watch,
    Modem,
    Cable,
}

public class BackPackManager : MonoBehaviour
{

    public static BackPackManager Instance
    {
        get; private set;
    }

    //public bool HaveDrill
    //{
    //    get { return m_haveDrill;}
    //}

    //public bool HaveModem
    //{
    //    get { return m_haveModem;}
    //}

    //public int CableCount
    //{
    //    get { return m_CableCount;}
    //}
    public RaialMenuManager m_RadialMenuManager;

    private bool m_haveBelt;
    private bool m_haveDrill;
    private bool m_haveWatch;
    private bool m_haveModem;

    private int m_CableCount = 0;


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
        if (m_RadialMenuManager == null)
        {
            transform.parent.GetComponentInChildren<RaialMenuManager>();
        }
    }

    public void CollectItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Default:
                Debug.LogWarning("Item Type not specified");
                break;
            case ItemType.Belt:
                m_haveBelt = true;
                CheckReady();
                EventCenter.Broadcast(EventDefine.BeltWear);
                break;
            case ItemType.Drill:
                m_haveDrill = true;
                CheckReady();
                UpdateRadiaMenu();
                Debug.Log("Drill collected!");
                break;
            case ItemType.Watch:
                m_haveWatch = true;
                CheckReady();
                Debug.Log("Watch collected!");
                break;
            case ItemType.Modem:
                m_haveModem = true;
                CheckReady();
                UpdateRadiaMenu();
                Debug.Log("Modem collected!");
                break;
            case ItemType.Cable:
                m_CableCount++;
                CheckReady();
                UpdateRadiaMenu();
                Debug.Log(string.Format("Cable collected! current cout : {0}", m_CableCount));
                break;
        }
    }

    public bool ReleaseItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Default:
                Debug.LogWarning("Item Type not specified");
                return false;
                break;
            case ItemType.Drill:
                if (m_haveDrill == true)
                {
                    m_haveDrill = false;
                    UpdateRadiaMenu();
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case ItemType.Modem:
                if (m_haveModem == true)
                {
                    m_haveModem = false;
                    UpdateRadiaMenu();
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case ItemType.Cable:
                if (m_CableCount > 0)
                {
                    m_CableCount--;
                    UpdateRadiaMenu();
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            default:
                return false;
                break;
        }
    }

    private void CheckReady()
    {
        if (m_haveBelt && m_haveDrill && m_haveWatch && m_haveModem && m_CableCount > 0) // player picks up items and ready to go
        {
            Lv1Manager.Instance.m_IsEntgerStep2 = true;
        }
    }

    private void UpdateRadiaMenu()
    {
        if (m_RadialMenuManager?.RadialMenu != null)
        {
            m_RadialMenuManager.RadialMenu.RegenerateButtons(new Color?[3] { m_haveModem ? (Color?)Color.green : null, m_haveDrill ? (Color?)Color.green : null, m_CableCount > 0 ? (Color?)Color.green : null });
        }
    }

}
