using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStepController : ItemStepController_Basic
{
    private void Awake()
    {
        EventCenter.AddListener(Steps[0], SetActive);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(Steps[0], SetActive);
    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }
}
