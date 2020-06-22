using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadsetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HeadSetContactPoint"))
        {
            EventCenter.Broadcast(EventDefine.LoadLevel, 1);
        }
    }
}
