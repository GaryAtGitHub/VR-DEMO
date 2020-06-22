using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSetStepController : ItemStepController_Basic
{
    private HeadSetController headSetController;
    private void Awake()
    {
        headSetController = GetComponent<HeadSetController>();
        EventCenter.AddListener(Steps[0], headSetController.Activate);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(Steps[0], headSetController.Activate);
    }

}
