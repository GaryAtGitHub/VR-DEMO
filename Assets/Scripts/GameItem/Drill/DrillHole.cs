using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DrillHole : MonoBehaviour
{
    public Transform m_DrillHead;
    public GameObject m_Hole;
    public VRTK_PolicyList m_PolicyList;
    public Color m_DisableColor;

    [SerializeField]
    [Range(0,0.5f)]
    private float m_DrillLength = 0.1f;
    private bool m_Drillable = false;
    private bool m_IsDrill = false;
    private Vector3 m_HolePosition;
    private RaycastHit m_hitFront;
    private RaycastHit m_hitBack;
    private VRTK_InteractableObject m_InteractableObject;
    private Material m_Indicator;    
    private DrillZoneController m_DrillZoneController;
    private GameObject Fronthole;
    private GameObject Backhole;

    private void OnEnable()
    {
        if (m_PolicyList == null)
        {
            m_PolicyList = GameObject.FindGameObjectWithTag("CabelPolicy").GetComponent<VRTK_PolicyList>();
        }
        m_Indicator = transform.Find("Indicator").GetComponent<MeshRenderer>().material;
        m_InteractableObject = (m_InteractableObject == null ? GetComponent<VRTK_InteractableObject>() : m_InteractableObject);
        if (m_InteractableObject != null)
        {
            m_InteractableObject.InteractableObjectUsed += M_InteractableObject_InteractableObjectUsed;
        }
        
    }

    private void OnDisable()
    {
        if (m_InteractableObject != null)
        {
            m_InteractableObject.InteractableObjectUsed -= M_InteractableObject_InteractableObjectUsed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrillZone")
        {
            if (m_Indicator != null)
            {
                m_Indicator.color = Color.green;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DrillZone")
        {
            m_Drillable = true;
            m_DrillZoneController = other.GetComponentInParent<DrillZoneController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DrillZone")
        {
            m_Drillable = false;
            m_Indicator.color = m_DisableColor;
        }
        m_DrillZoneController = null;
    }

    private void FixedUpdate()
    {
        if(m_Drillable)
        {
            if (Physics.Raycast(m_DrillHead.position, m_DrillHead.forward, out m_hitFront, m_DrillLength, 1 << 8, QueryTriggerInteraction.Ignore))
            {
                m_HolePosition = m_hitFront.point;
                m_IsDrill = true;
            }
            else
            {
                m_IsDrill = false;
            }

        }
    }

    private void M_InteractableObject_InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if(m_Drillable)
        {
            Drill();
        }
    }

    private void Drill()
    {
        if (m_IsDrill)
        {
            if (Fronthole != null)
            {
                Destroy(Fronthole);
            }
            if (Backhole != null)
            {
                Destroy(Backhole);
            }
            if (m_DrillZoneController?.m_DrillFrontHoleParent != null)
            {
                Fronthole = Instantiate(m_Hole, m_HolePosition, Quaternion.LookRotation(m_hitFront.normal * -1), m_DrillZoneController.m_DrillFrontHoleParent);
                StartCoroutine(m_DrillZoneController.m_DrillFrontHoleParent.GetComponent<PairController>().UpdateSnapZone());
            }
            else
            {
                Fronthole = Instantiate(m_Hole, m_HolePosition, Quaternion.LookRotation(m_hitFront.normal * -1));
            }
            Fronthole.GetComponent<VRTK_SnapDropZone>().validObjectListPolicy = m_PolicyList;
            Physics.Raycast(m_hitFront.point, m_hitFront.normal * -1, out m_hitBack, 1, 1 << 9, QueryTriggerInteraction.Ignore);
            if (m_DrillZoneController?.m_DrillRearHoleParent != null)
            {
                Backhole = Instantiate(m_Hole, m_hitBack.point, Quaternion.LookRotation(m_hitFront.normal), m_DrillZoneController.m_DrillRearHoleParent);
            }
            else
            {
                Backhole = Instantiate(m_Hole, m_hitBack.point, Quaternion.LookRotation(m_hitFront.normal));
            }            
            Backhole.GetComponent<VRTK_SnapDropZone>().validObjectListPolicy = m_PolicyList;
        }
    }

}
