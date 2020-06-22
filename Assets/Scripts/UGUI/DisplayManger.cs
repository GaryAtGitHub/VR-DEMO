using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayManger : MonoBehaviour
{
    private TextMeshProUGUI text; 

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateDisplay(int num)
    {
        text.text = text.text + num.ToString();
    }

    public void BackSpace()
    {
        text.text = text.text.Remove(text.text.Length - 1, 1);
    }

    public void ClearAll()
    {
        text.text = "";
    }
}
