using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_UpdateBigScreen : MonoBehaviour
{
    public int level;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            EventCenter.Broadcast(EventDefine.UpdateBigScreen, level);
        } );
    }
}
