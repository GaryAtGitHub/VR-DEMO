using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class ModemInstallationManager : MonoBehaviour
{
    public EventDefine m_ActivateStep;
    public InventorySpawnInHand m_spawanInHand;

    private bool isCoroutineRuning;
    private bool isModemPlace;
    private bool isWallCableConnected;
    private bool isModemCableConnected;
    private bool isDisplaySetUp;
    private Transform cableToModem;
    private Transform cableToWall;
    private VRTK_SnapDropZone modemPlaceZone;
    private VRTK_SnapDropZone modemConnector;
    private GameObject Modem;
    private IEnumerator DisplayCorotine;
    private Color origineBlue;
    private Color origineGreen;

    private void Start()
    {
        modemPlaceZone = GetComponentInChildren<VRTK_SnapDropZone>();
        if (m_spawanInHand == null)
        {
            m_spawanInHand = FindObjectOfType<InventorySpawnInHand>();
        }
        modemPlaceZone.gameObject.SetActive(false);
        m_spawanInHand.ModemSpawn += RegisterModem;
        EventCenter.AddListener(m_ActivateStep, ActivateSection);
        //EventCenter.AddListener(m_ActivateStep, RegisterCableSnapZone);
        if (modemPlaceZone != null)
        {
            modemPlaceZone.ObjectSnappedToDropZone += ModemSnaptoZone;
            modemPlaceZone.ObjectUnsnappedFromDropZone += ModemUnSnaptoZone;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(m_ActivateStep, ActivateSection);
        //EventCenter.RemoveListener(m_ActivateStep, RegisterCableSnapZone);
        UnRegisterCableSnapZone();
        if (modemPlaceZone != null)
        {
            modemPlaceZone.ObjectSnappedToDropZone -= ModemSnaptoZone;
            modemPlaceZone.ObjectUnsnappedFromDropZone -= ModemUnSnaptoZone;
        }
        m_spawanInHand.ModemSpawn -= RegisterModem;
    }

    private void ActivateSection()
    {
        modemPlaceZone.gameObject.SetActive(true);
        RegisterCableSnapZone();
        gameObject.SetActive(true);
    }

    private void ModemSnaptoZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Modem Place!");
        isModemPlace = true;
        CheckIsSetUp();
    }


    private void ModemUnSnaptoZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Modem Removed!");
        isModemPlace = false;
    }

    private void RegisterModem(GameObject spawnedObject)
    {
        if (!gameObject.activeSelf) return;
        VRTK_SnapDropZone modemConnector = spawnedObject.GetComponentInChildren<VRTK_SnapDropZone>(true);
        Modem = spawnedObject;
        modemConnector.gameObject.SetActive(true);
        if (modemConnector != null)
        {
            modemConnector.ObjectSnappedToDropZone += ModemInstallationManager_ObjectSnappedToDropZone;
            modemConnector.ObjectUnsnappedFromDropZone += ModemInstallationManager_ObjectUnsnappedFromDropZone;
        }

    }

    private void ModemInstallationManager_ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        isModemCableConnected = false;
        Debug.Log("Cable Disonnected from Modem!");
        if (DisplayCorotine != null && isCoroutineRuning)
        {
            StopCoroutine(DisplayCorotine);
        }
        ResetDisplay();
        cableToModem = null;
    }

    private void ModemInstallationManager_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        isModemCableConnected = true;
        Debug.Log("Cable Connected to Modem!");
        cableToModem = e.snappedObject.GetComponent<VRTK_InteractableObject>()?.GetPreviousParent;
        CheckIsSetUp();
    }

    private void RegisterCableSnapZone()
    {
        VRTK_SnapDropZone[] SnapZones = GetComponentsInChildren<VRTK_SnapDropZone>();
        foreach (var snapZone in SnapZones)
        {
            if (snapZone != modemPlaceZone)
            {
                snapZone.ObjectSnappedToDropZone += SnapZone_ObjectSnappedToDropZone;
                snapZone.ObjectUnsnappedFromDropZone += SnapZone_ObjectUnsnappedFromDropZone;
            }
        }
    }

    private void UnRegisterCableSnapZone()
    {
        VRTK_SnapDropZone[] SnapZones = GetComponentsInChildren<VRTK_SnapDropZone>();
        foreach (var snapZone in SnapZones)
        {
            if (snapZone != modemPlaceZone)
            {
                snapZone.ObjectSnappedToDropZone -= SnapZone_ObjectSnappedToDropZone;
                snapZone.ObjectUnsnappedFromDropZone -= SnapZone_ObjectUnsnappedFromDropZone;
            }
        }
    }

    private void SnapZone_ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        isWallCableConnected = false;
        Debug.Log("Cable Disconnected from Wall!");
        if (DisplayCorotine != null && isCoroutineRuning)
        {
            StopCoroutine(DisplayCorotine);
        }
        ResetDisplay();
        cableToWall = null;
    }

    private void SnapZone_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        isWallCableConnected = true;
        Debug.Log("Cable Connected to Wall!");
        cableToWall = e.snappedObject.transform.parent;
        CheckIsSetUp();
    }

    private void CheckIsSetUp()
    {
        if (isWallCableConnected && isModemCableConnected && cableToWall == cableToModem)
        {
            Debug.Log("Modem Set Up");
            if(isDisplaySetUp != true)
            {
                isDisplaySetUp = true;
                if (Modem != null)
                {
                    Text DisplayText = Modem.GetComponentInChildren<Text>();
                    Debug.Log("TryGettingText");
                    if (DisplayText != null)
                    {
                        Debug.Log("TextGot!!");
                        DisplayCorotine = Display(DisplayText);
                        StartCoroutine(DisplayCorotine);
                    }
                }
            }
            if (isModemPlace)
            {
                Debug.Log("Modem Set Up and Placed!!");
                Lv1Manager.Instance.m_IsEnterStep6 = true;
            }
        }
    }

    private IEnumerator Display(Text DisplayText)
    {
        isCoroutineRuning = true;
        Material blue = Modem.transform.Find("Lights/Light (2)").GetComponent<MeshRenderer>().material;
        Material green = Modem.transform.Find("Lights/Light (1)").GetComponent<MeshRenderer>().material;
        origineBlue = blue.color;
        origineGreen = green.color;
        for (int i = 0; i < 10; i++)
        {
            if (i % 4 != 0)
            {
                blue.color = Color.blue;
                blue.SetFloat("_Metallic", 1);
                DisplayText.text = DisplayText.text + "..";
            }
            else
            {
                blue.color = origineBlue;
                DisplayText.text = "";
                blue.SetFloat("_Metallic", 0);
            }
            yield return new WaitForSeconds(0.5f);
        }
        blue.color = origineBlue;
        blue.SetFloat("_Metallic", 1);
        green.color = Color.green;
        DisplayText.text = "Set Up Successful!";
        isCoroutineRuning = false;
    }

    private void ResetDisplay()
    {
        isDisplaySetUp = false;
        Text DisplayText = Modem?.GetComponentInChildren<Text>();
        Material blue = Modem?.transform.Find("Lights/Light (2)").GetComponent<MeshRenderer>().material;
        Material green = Modem?.transform.Find("Lights/Light (1)").GetComponent<MeshRenderer>().material;
 
        if (DisplayText != null)
        {
            DisplayText.text = "";
        }

        if (blue != null && origineBlue != null)
        {
            blue.color = origineBlue;
        }
        if (green != null && origineGreen != null)
        {
            green.color = origineGreen;
        }

    }


}
