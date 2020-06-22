using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PairController : MonoBehaviour
{
    public CableAndDrillZoneManager TriggersManager;

    private VRTK_SnapDropZone[] SnapZones;
    private GameObject previousSnappedObject = null;

    private void Start()
    {
        if (TriggersManager == null) return;
        RegisterListener();
    }

    private void OnDestroy()
    {
        UnRegisterListener();
    }

    private void SnapZone_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        if (previousSnappedObject == null)
        {
            previousSnappedObject = e.snappedObject;
        }
        else
        {
            if (e.snappedObject.transform.parent == previousSnappedObject.transform.parent)
            {
                foreach (var snapZone in SnapZones)
                {
                    previousSnappedObject.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
                    e.snappedObject.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
                }
                TriggersManager.TriggerUpdate(gameObject);
            }
        }

    }


    private void SnapZone_ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        previousSnappedObject = null;
    }

    public IEnumerator UpdateSnapZone()
    {
        UnRegisterListener();
        yield return null;

        RegisterListener();
    }

    private void RegisterListener()
    {
        SnapZones = GetComponentsInChildren<VRTK_SnapDropZone>();
        if (SnapZones.Length > 2)
        {
            throw new Exception("SnapZones are more than 2");
        }

        foreach (var snapZone in SnapZones)
        {
            snapZone.ObjectSnappedToDropZone += SnapZone_ObjectSnappedToDropZone;
            snapZone.ObjectUnsnappedFromDropZone += SnapZone_ObjectUnsnappedFromDropZone;
        }
    }

    private void UnRegisterListener()
    {
        foreach (var snapZone in SnapZones)
        {
            if (snapZone != null)
            {
                snapZone.ObjectSnappedToDropZone -= SnapZone_ObjectSnappedToDropZone;
                snapZone.ObjectUnsnappedFromDropZone -= SnapZone_ObjectUnsnappedFromDropZone;
            }
        }
    }
}
