using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRopeLine : MonoBehaviour
{
    public float ropeWidth;

    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] positions = new Vector3[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            positions[i] = child.position;
            i++;
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

    }
}
