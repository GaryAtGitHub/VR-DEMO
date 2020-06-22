using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpwan : MonoBehaviour
{
    [SerializeField]
    public GameObject partPrefab, parentObject; 

    [SerializeField]
    [Range(1, 1000)]
    private float lenght = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast; 

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Rope"))
            {
                Destroy(temp);
            }

            reset = false;
        }

        if (spawn)
        {
            Spawn();

            spawn = false;
        }
    }

    public void Spawn()
    {
        int count = (int)(lenght / partDistance);

        for (int i = 0; i< count; i++)
        {
            GameObject temp;

            temp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y- i* partDistance, transform.position.z), Quaternion.identity, parentObject.transform);
            //temp.transform.eulerAngles = new Vector3(180, 0, 0);

            temp.name = parentObject.transform.childCount.ToString();

            ConfigurableJoint characterJoint = temp.GetComponent<ConfigurableJoint>();
            if (i == 0)
            {
                Destroy(characterJoint);
                temp.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                characterJoint.connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
