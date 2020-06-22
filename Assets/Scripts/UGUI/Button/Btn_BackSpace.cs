using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_BackSpace : Btn_Basic
{
    private new void Start()
    {
        base.Start();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            DisplayManager.BackSpace();
        });
    }


}
