using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CableSnapController : MonoBehaviour
{
    private VRTK_SnapDropZone snapZone;
    private void Start()
    {
        snapZone = GetComponent<VRTK_SnapDropZone>();
        if (snapZone != null)
        {
            snapZone.ObjectSnappedToDropZone += SnapZone_ObjectSnappedToDropZone;
        }
    }

    private void OnDestroy()
    {
        if (snapZone != null)
        {
            snapZone.ObjectSnappedToDropZone -= SnapZone_ObjectSnappedToDropZone;
        }
    }

    private void SnapZone_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        if (e.snappedObject.name == "Start")
        {
            e.snappedObject.transform.Rotate(0, -90, 0);
        }
        else if (e.snappedObject.name == "End")
        {
            e.snappedObject.transform.Rotate(0, 90, 0);
        }
    }

}
