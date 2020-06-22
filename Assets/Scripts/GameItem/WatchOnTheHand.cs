using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchOnTheHand : MonoBehaviour
{
    private void Start()
    {        
        EventCenter.AddListener(EventDefine.WatchWear, SetActive);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.WatchWear, SetActive);
    }


    private void SetActive()
    {
        gameObject.SetActive(true);
    }
}
