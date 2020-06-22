using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSwitchButton : MonoBehaviour
{
    private PageManager pageManager;
    private int currentPageIndex;
    public int nextPageIndex;

    private void Start()
    {
        pageManager = GetComponentInParent<PageManager>();
        currentPageIndex = transform.parent.GetSiblingIndex();
        GetComponent<Button>().onClick.AddListener(delegate { pageManager.SwitchPage(currentPageIndex, nextPageIndex); });
    }

}
