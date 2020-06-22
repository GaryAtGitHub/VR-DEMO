using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Num : Btn_Basic
{
    public int ButtonNum;

    private new void Start()
    {
        base.Start();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            DisplayManager.UpdateDisplay(ButtonNum);
        });        
    }

}
