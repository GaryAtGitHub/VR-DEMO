using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_ClearAll : Btn_Basic
{
    private new void Start()
    {
        base.Start();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            DisplayManager.ClearAll();
        });
    }

}
