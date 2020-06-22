using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class StraightPointerManager : MonoBehaviour
{
    public Color EnterColor, SetColor, ExitColor;
    private VRTK_Pointer pointer;

    private void Awake()
    {
        pointer = GetComponent<VRTK_Pointer>();
        pointer.DestinationMarkerEnter += Pointer_DestinationMarkerEnter;
        pointer.DestinationMarkerExit += Pointer_DestinationMarkerExit;
        pointer.DestinationMarkerSet += Pointer_DestinationMarkerSet;
    }

    private void OnDestroy()
    {
        pointer.DestinationMarkerEnter -= Pointer_DestinationMarkerEnter;
        pointer.DestinationMarkerExit -= Pointer_DestinationMarkerExit;
        pointer.DestinationMarkerSet -= Pointer_DestinationMarkerSet;
    }

    private void Pointer_DestinationMarkerSet(object sender, DestinationMarkerEventArgs e)
    {
        HightLight(e.target, SetColor);
    }

    private void Pointer_DestinationMarkerExit(object sender, DestinationMarkerEventArgs e)
    {
        HightLight(e.target, Color.clear);
    }

    private void Pointer_DestinationMarkerEnter(object sender, DestinationMarkerEventArgs e)
    {
        HightLight(e.target, EnterColor);
    }

    private void HightLight(Transform target, Color color)
    {
        VRTK_BaseHighlighter hightLighter = (target != null ? target.GetComponent<VRTK_BaseHighlighter>() : null);
        if (hightLighter != null)
        {
            hightLighter.Initialise();
            if (color != Color.clear)
            {
                hightLighter.Highlight(color);
            }
            else
            {
                hightLighter.Unhighlight();
            }
        }
       
    }
}
