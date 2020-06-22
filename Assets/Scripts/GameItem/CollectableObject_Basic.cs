using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CollectableObject_Basic : MonoBehaviour
{
    public ItemType type;
    public bool IsCollectable = true;
    public GameObject CustomDestroyGameObject = null;

    private VRTK_InteractableObject m_InteractableObject;

    private void Start()
    {
        m_InteractableObject = GetComponent<VRTK_InteractableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsCollectable) return;
        if (m_InteractableObject.IsGrabbed())
        {            
            if (other.transform.parent.tag == "Player")
            {
                BackPackManager.Instance.CollectItem(type);
                m_InteractableObject.GetGrabbingObject().GetComponent<VRTK_InteractGrab>().ForceRelease();
                if (CustomDestroyGameObject != null)
                {
                    Destroy(CustomDestroyGameObject);
                }
                else
                {
                    Destroy(gameObject);
                }                
            }
        }
    }
}
