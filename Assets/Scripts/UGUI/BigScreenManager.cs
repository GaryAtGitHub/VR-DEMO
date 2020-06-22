using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigScreenManager : MonoBehaviour
{
    int currentPage = 0;

    private void Start()
    {
        EventCenter.AddListener<int>(EventDefine.UpdateBigScreen, UpdateBigSreen);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.UpdateBigScreen, UpdateBigSreen);
    }

    private void UpdateBigSreen(int nextPage)
    {
        if (currentPage == nextPage) return;
        if (currentPage != 0)
        {
            transform.GetChild(currentPage).DOLocalMoveY(2.2f, 0.5F);
        }
        transform.GetChild(nextPage).DOLocalMoveY(0, 0.5F);
        currentPage = nextPage;

    }
}
