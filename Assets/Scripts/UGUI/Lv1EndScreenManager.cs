using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lv1EndScreenManager : MonoBehaviour
{
    public EventDefine ActivateStep;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
        EventCenter.AddListener(ActivateStep, Activate);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(ActivateStep, Activate);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        rectTransform.position = new Vector3 (rectTransform.position.x, rectTransform.position.y + 20, rectTransform.position.z);
        rectTransform.DOLocalMoveY(6, 1.5f);
    }

}
