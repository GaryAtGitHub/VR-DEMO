using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class Watch : MonoBehaviour
{
    public EventDefine ActiveStep;
    public Color HightlightColor;

    private VRTK_OutlineObjectCopyHighlighter Highliter;
    private VRTK_InteractableObject InteractableObject;

    private void Start()
    {
        Highliter = GetComponent<VRTK_OutlineObjectCopyHighlighter>();
        InteractableObject = GetComponent<VRTK_InteractableObject>();
        InteractableObject.InteractableObjectGrabbed += InteractableObject_InteractableObjectGrabbed;
        EventCenter.AddListener(ActiveStep, Init);
    }

    private void OnDestroy()
    {
        InteractableObject.InteractableObjectGrabbed -= InteractableObject_InteractableObjectGrabbed;
        EventCenter.RemoveListener(ActiveStep, Init);
    }


    private void InteractableObject_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        EventCenter.Broadcast(EventDefine.WatchWear);
        BackPackManager.Instance.CollectItem(ItemType.Watch);
        Destroy(gameObject);
    }

    private void Init()
    {
        if (Highliter != null)
        {
            Highliter.Initialise();
            Highliter.Highlight(HightlightColor);
        }       
    }
}
