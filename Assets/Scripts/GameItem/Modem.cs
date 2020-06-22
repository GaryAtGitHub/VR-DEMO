using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// This script is for modem assembly
/// </summary>
public class Modem : MonoBehaviour
{
    private VRTK_SnapDropZone[] SnapZones;
    private int NumofSnapped = 0;

    private void Start()
    {
        SnapZones = GetComponentsInChildren<VRTK_SnapDropZone>();
        foreach (var snapZone in SnapZones)
        {
            if (snapZone != null)
            {
                snapZone.ObjectSnappedToDropZone += AddSnapCount;
                snapZone.ObjectUnsnappedFromDropZone += MinusSnapCount;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var snapZone in SnapZones)
        {
            if (snapZone != null)
            {
                snapZone.ObjectSnappedToDropZone -= AddSnapCount;
                snapZone.ObjectUnsnappedFromDropZone -= MinusSnapCount;
            }            
        }
    }

    private void AddSnapCount(object sender, SnapDropZoneEventArgs e)
    {
        NumofSnapped++;
        CheckSnapStatus();
    }

    private void MinusSnapCount(object sender, SnapDropZoneEventArgs e)
    {
        if (NumofSnapped == SnapZones.Length)
        {
            Destroy(GetComponent<CollectableObject_Basic>());
        }
        NumofSnapped--;
        CheckSnapStatus();
    }

    private void CheckSnapStatus()
    {
        if (NumofSnapped < SnapZones.Length) return;
        CollectableObject_Basic Collectable = gameObject.AddComponent<CollectableObject_Basic>();
        Collectable.IsCollectable = true;
        Debug.Log("Collectable Set true");
        Collectable.type = ItemType.Modem;
    }
}
