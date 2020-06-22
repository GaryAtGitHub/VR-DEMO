using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public void SwitchPage(int currentPage, int nextPage)
    {
        transform.GetChild(currentPage).gameObject.SetActive(false);
        transform.GetChild(nextPage).gameObject.SetActive(true);
    }
}
