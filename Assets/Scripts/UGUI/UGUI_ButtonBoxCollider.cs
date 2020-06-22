using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUI_ButtonBoxCollider : MonoBehaviour
{
    BoxCollider boxCollider;
    RectTransform rectTransform;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rectTransform = GetComponent<RectTransform>();
        if (boxCollider == null)
        {           
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }
        StartCoroutine(Resize());
    }

    IEnumerator Resize()
    {
        yield return new WaitForEndOfFrame();
        boxCollider.size = new Vector3(rectTransform.rect.width, rectTransform.rect.height, 1);
        Destroy(this);
    }
}
