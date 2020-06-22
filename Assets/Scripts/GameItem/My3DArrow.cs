using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My3DArrow : MonoBehaviour
{
    public GameObject follow;
    [SerializeField]
    [Range(0,2)]
    private float hoverHeight = 0.5f;
    [SerializeField]
    [Range(0, 180)]
    private float spinSpeed = 90;
    [SerializeField]
    [Range(0, 5)]
    private float spinTime = 2;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = 
            new Vector3(follow.transform.position.x , follow.transform.position.y + hoverHeight + 0.3f * hoverHeight * Mathf.Sin(2 * Mathf.PI * Time.time / spinTime), follow.transform.position.z);
        transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
    }
}
