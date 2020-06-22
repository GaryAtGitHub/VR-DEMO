using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class TestRopePin : MonoBehaviour
{
    public ObiRope TestRope;
    public int particledex;
    private string NameOfParticleLast;
    private string NameOfParticleFirst;

    int LastParticleConstrainIndex;
    int SecondLastParticleConstrainIndex;
    int FirstParticleConstrainIndex;
    int SecondParticleConstrainIndex;
    ObiPinConstraintBatch pinConstraintBatch;
    ObiRope obiRope;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Cable")) return;
        ObiCollider obicollider = other.GetComponent<ObiCollider>();
        obiRope  = other.transform.parent.GetComponentInChildren<ObiRope>();
        ObiPinConstraints pinConstrain = other.transform.parent.GetComponentInChildren<ObiPinConstraints>();
        pinConstraintBatch = pinConstrain.GetFirstBatch();

        Debug.Log("Total Particle" + obiRope.TotalParticles);
        Debug.Log("Used Particle" + obiRope.UsedParticles);
        Debug.Log("Pooled Particle" + obiRope.PooledParticles);

        foreach (var item in pinConstraintBatch.pinBodies)
        {
            Debug.Log(item.gameObject.name);
        }

        foreach (var item in pinConstraintBatch.pinIndices)
        {
            Debug.Log(item);
        }

        foreach (var item in pinConstraintBatch.GetConstraintsInvolvingParticle(particledex))
        {
            Debug.Log("Constrain of Last pariticle" + item);
        }

        pinConstrain.RemoveFromSolver(null);
        UpdateIndex();

        int CheckPoint = 0;
        Debug.Log("CheckPoin" + CheckPoint++);
        UpdateIndex();
        NameOfParticleLast = pinConstraintBatch.pinBodies[LastParticleConstrainIndex].gameObject.name;
        NameOfParticleFirst = pinConstraintBatch.pinBodies[FirstParticleConstrainIndex].gameObject.name;

        if (other.name == NameOfParticleLast)
        {
            pinConstraintBatch.RemoveConstraint(LastParticleConstrainIndex);
            pinConstraintBatch.AddConstraint(obiRope.UsedParticles - 1, GetComponent<ObiColliderBase>(),
                                                          transform.InverseTransformPoint(obiRope.GetParticlePosition(TestRope.UsedParticles - 1)), Quaternion.identity, 0);
            UpdateIndex();
            Debug.Log("CheckPoin" + CheckPoint++);
            pinConstraintBatch.RemoveConstraint(SecondLastParticleConstrainIndex);           
            pinConstraintBatch.AddConstraint(obiRope.UsedParticles - 2, GetComponent<ObiColliderBase>(),
                                                          transform.InverseTransformPoint(obiRope.GetParticlePosition(TestRope.UsedParticles - 2)), Quaternion.identity, 0);
            Debug.Log("CheckPoin" + CheckPoint++);
        }
        else if (other.name == NameOfParticleFirst)
        {

                pinConstraintBatch.RemoveConstraint(FirstParticleConstrainIndex);
                pinConstraintBatch.AddConstraint(0, GetComponent<ObiColliderBase>(),
                                              transform.InverseTransformPoint(obiRope.GetParticlePosition(0)), Quaternion.identity, 0);
                Debug.Log("CheckPoin" + CheckPoint++);
                UpdateIndex();
                pinConstraintBatch.RemoveConstraint(SecondParticleConstrainIndex);
                pinConstraintBatch.AddConstraint(1, GetComponent<ObiColliderBase>(),
                                                              transform.InverseTransformPoint(obiRope.GetParticlePosition(1)), Quaternion.identity, 0);
        }

        pinConstrain.AddToSolver(null);
    }

    private void UpdateIndex()
    {
        LastParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(obiRope.UsedParticles - 1)[0];
        SecondLastParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(obiRope.UsedParticles - 2)[0];
        FirstParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(0)[0];
        SecondParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(1)[0];

    }

    private void Update()
    {
        Debug.Log("Start!!!!!!!");
        if (TestRope.GetParticlePosition(0) == null)
            Debug.Log("Positions null");
        Debug.Log(TestRope.positions.Length);
            Debug.Log(TestRope.GetParticlePosition(0));
        Debug.Log(TestRope.GetParticlePosition(TestRope.UsedParticles-1));
        Debug.Log(TestRope.GetParticlePosition(TestRope.UsedParticles ));
        Debug.Log("End!!!!!!!");
    }
}
