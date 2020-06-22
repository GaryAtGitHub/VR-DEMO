using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lower Priority. Do it when have time
public class InteractableItemsManager : MonoBehaviour
{

    public GameObject[] CollectableObjects;

    //public EventDefine Step;
    private void Start()
    {
        EventCenter.AddListener(EventDefine.BeltWear, SetCollectable);
        //EventCenter.AddListener(Step, ProceduralHighLigh);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.BeltWear, SetCollectable);
        //EventCenter.RemoveListener(Step, ProceduralHighLigh);
    }

    private void SetCollectable()
    {
        foreach (var go in CollectableObjects)
        {
            if (go != null)
            {
                CollectableObject_Basic Collectable = go.GetComponent<CollectableObject_Basic>();
                if (Collectable != null)
                {
                    Collectable.IsCollectable = true;
                }
                else
                {
                    CollectableObject_Basic[] Collectables = go.GetComponentsInChildren<CollectableObject_Basic>();
                    foreach (var item in Collectables)
                    {
                        item.IsCollectable = true;
                    }
                }
            }           

        }
    }

    //private void ProceduralHighLigh()
    //{
    //    foreach (Transform child in transform)
    //    {

    //    }
    //}
}
