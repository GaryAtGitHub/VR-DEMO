using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public delegate void SpawanEventHandeler(GameObject spawnedObject);

public class InventorySpawnInHand : MonoBehaviour
{
    public VRTK_InteractTouch m_InteractTouch;
    public VRTK_InteractGrab m_InteractGrab;
    public GameObject m_ModemPrefab;
    public GameObject m_DrillPrefab;
    public GameObject m_CablePrefab;
    public Transform m_Grabpoint;

    public event SpawanEventHandeler ModemSpawn;

    private GameObject m_SpawnObject;
   
    //private bool m_IsClearSpawnObject;
    
    private void Start()
    {
        if (m_InteractTouch == null)
        {
            m_InteractTouch = GetComponent<VRTK_InteractTouch>();
        }

        if (m_InteractGrab == null)
        {
            m_InteractGrab = GetComponent<VRTK_InteractGrab>();
        }

        EventCenter.AddListener(EventDefine.SpawnModem, SpwanModem);
        EventCenter.AddListener(EventDefine.SpawnDrill, SpwanDrill);
        EventCenter.AddListener(EventDefine.SpawnCable, SpwanCable);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.SpawnModem, SpwanModem);
        EventCenter.RemoveListener(EventDefine.SpawnDrill, SpwanDrill);
        EventCenter.RemoveListener(EventDefine.SpawnCable, SpwanCable);
    }


    private void SpwanModem()
    {
        if (BackPackManager.Instance.ReleaseItem(ItemType.Modem))
        {
            GameObject go = SpawnInHand(m_ModemPrefab);
            foreach (var item in go.GetComponentsInChildren<CollectableObject_Basic>())
            {
                item.IsCollectable = true;
            }
            ModemSpawn(go);
        }
    }

    private void SpwanDrill()
    {
        if (BackPackManager.Instance.ReleaseItem(ItemType.Drill))
        {
            GameObject go = SpawnInHand(m_DrillPrefab);
            foreach (var item in go.GetComponentsInChildren<CollectableObject_Basic>())
            {
                item.IsCollectable = true;
            }
        }
    }

    private void SpwanCable()
    {
        if (BackPackManager.Instance.ReleaseItem(ItemType.Cable))
        {
            GameObject go = SpawnInHand(m_CablePrefab);
            foreach (var item in go.GetComponentsInChildren<CollectableObject_Basic>())
            {
                item.IsCollectable = true;
            }
        }
    }

    private GameObject SpawnInHand(GameObject go)
    {
        if (m_InteractGrab.GetGrabbedObject() != null)
        {
            m_InteractGrab.ForceRelease();
        }

        //if (m_SpawnObject != null & m_IsClearSpawnObject == true)
        //{
        //    Destroy(m_SpawnObject);
        //}
        m_SpawnObject = Instantiate(go);
        VRTK_InteractableObject interactableObject = m_SpawnObject.GetComponent<VRTK_InteractableObject>();
        interactableObject = interactableObject? interactableObject: m_SpawnObject.GetComponentInChildren<VRTK_InteractableObject>();
        //m_IsClearSpawnObject = true;

        if (interactableObject.isGrabbable && !interactableObject.IsGrabbed())
        {           
            m_SpawnObject.transform.position = m_Grabpoint.transform.position;
            m_InteractTouch.ForceTouch(interactableObject.gameObject);
            m_InteractGrab.AttemptGrab();
        }
        return m_SpawnObject;
    }
}
