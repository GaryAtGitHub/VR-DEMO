using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class Cable : MonoBehaviour
{
    private VRTK_InteractableObject[] InteractableObjects;
    private ObiRope obiRope;
    private ObiPinConstraints pinConstrain;
    private ObiPinConstraintBatch pinConstraintBatch;
    private ObiSolver obiSolver;

    private int LastParticleConstrainIndex;
    private int SecondLastParticleConstrainIndex;
    private int FirstParticleConstrainIndex;
    private int SecondParticleConstrainIndex;

    private string NameOfParticleLast;
    private string NameOfParticleFirst;

    private void Awake()
    {
        InteractableObjects = GetComponentsInChildren<VRTK_InteractableObject>();
        obiRope = GetComponentInChildren<ObiRope>();
        pinConstrain = GetComponentInChildren<ObiPinConstraints>();
        pinConstraintBatch = pinConstrain.GetFirstBatch();
        obiSolver = GetComponentInChildren<ObiSolver>();
        UpdateIndex();
        NameOfParticleLast = pinConstraintBatch.pinBodies[LastParticleConstrainIndex].gameObject.name;
        NameOfParticleFirst = pinConstraintBatch.pinBodies[FirstParticleConstrainIndex].gameObject.name;

        foreach (var interactObject in InteractableObjects)
        {
            interactObject.InteractableObjectGrabbed += InteractObject_InteractableObjectGrabbed;
            interactObject.InteractableObjectUngrabbed += InteractObject_InteractableObjectUngrabbed;
        }
    }

    private void OnDestroy()
    {
        foreach (var interactObject in InteractableObjects)
        {
            if (interactObject != null)
            {
                interactObject.InteractableObjectGrabbed -= InteractObject_InteractableObjectGrabbed;
                interactObject.InteractableObjectUngrabbed -= InteractObject_InteractableObjectUngrabbed;
            }
        }
    }

    private void InteractObject_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        VRTK_InteractableObject Sender = (VRTK_InteractableObject)sender;
        Sender.GetComponent<ObiRigidbody>().kinematicForParticles = false;
    }

    /// <summary>
    /// Set Grabbed the cable end to be kinematic to rope.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InteractObject_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        VRTK_InteractableObject Sender = (VRTK_InteractableObject)sender;
        Sender.GetComponent<ObiRigidbody>().kinematicForParticles = true;
    }

    /// <summary>
    /// Remove pin constraints of rope to prepare rope napping
    /// </summary>
    /// <param name="CableEnd"></param>One of the cabel ends that enter the snapZone
    /// <param name="isSnap"></param>Flag for snap of unsnap cable
    public void SnapCabel(GameObject CableEnd, bool isSnap = true)
    {
        pinConstrain.RemoveFromSolver(null);
        UpdateIndex();
        StartCoroutine(SetSnapCablePinConstraints(CableEnd, isSnap));
    }

    private IEnumerator SetSnapCablePinConstraints(GameObject CableEnd, bool isSnap)
    {
        ObiColliderBase obiCollider = CableEnd.GetComponent<ObiColliderBase>();
        if (CableEnd.name == NameOfParticleLast)
        {
            Vector3 LastParticlePinOffSet = pinConstraintBatch.pinOffsets[LastParticleConstrainIndex];
            Vector3 SecondLastParticlePinOffSet = pinConstraintBatch.pinOffsets[SecondLastParticleConstrainIndex];
            RemovePinConstraint(CableEnd, isSnap, ref LastParticleConstrainIndex, ref SecondLastParticleConstrainIndex);

            yield return null;

            AddPinConstraint(obiRope.UsedParticles - 1, obiRope.UsedParticles - 2, obiCollider, LastParticlePinOffSet, SecondLastParticlePinOffSet);
        }
        else if (CableEnd.name == NameOfParticleFirst)
        {
            Vector3 FirstParticlePinOffSet = pinConstraintBatch.pinOffsets[FirstParticleConstrainIndex];
            Vector3 SecondParticlePinOffSet = pinConstraintBatch.pinOffsets[SecondParticleConstrainIndex];
            RemovePinConstraint(CableEnd, isSnap, ref FirstParticleConstrainIndex, ref SecondParticleConstrainIndex);

            yield return null;

            AddPinConstraint(0, 1, obiCollider, FirstParticlePinOffSet, SecondParticlePinOffSet);
        }
        obiCollider.ParentChange();
        pinConstrain.AddToSolver(null);
    }

    private void RemovePinConstraint(GameObject CableEnd, bool isSnap, ref int ConstraintIndexOne, ref int ConstrainIndexTwo)
    {
        pinConstraintBatch.RemoveConstraint(ConstraintIndexOne);

        UpdateIndex();

        pinConstraintBatch.RemoveConstraint(ConstrainIndexTwo);

        UpdateIndex();

        if (isSnap == true)
        {
            Destroy(CableEnd.GetComponent<ObiRigidbody>());
        }
        else
        {
            if (CableEnd.GetComponent<ObiRigidbody>() == null)
            {
                CableEnd.AddComponent<ObiRigidbody>();
            }           
        }
    }

    private void AddPinConstraint(int ParticleIndexOne, int intParticleIndexTwo, ObiColliderBase obiCollider, Vector3 offSetOne, Vector3 offSetTwo)
    {
        pinConstraintBatch.AddConstraint(ParticleIndexOne, obiCollider, offSetOne, Quaternion.identity, 0);

        UpdateIndex();

        pinConstraintBatch.AddConstraint(intParticleIndexTwo, obiCollider, offSetTwo, Quaternion.identity, 0);
        UpdateIndex();
    }

    private void UpdateIndex()
    {
        LastParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(obiRope.UsedParticles - 1).Count != 0 ?
                                                        pinConstraintBatch.GetConstraintsInvolvingParticle(obiRope.UsedParticles - 1)[0] : -1;
        SecondLastParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(obiRope.UsedParticles - 2).Count != 0 ?
                                                        pinConstraintBatch.GetConstraintsInvolvingParticle(obiRope.UsedParticles - 2)[0] : -1;
        FirstParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(0).Count != 0 ?
                                                        pinConstraintBatch.GetConstraintsInvolvingParticle(0)[0] : -1;
        SecondParticleConstrainIndex = pinConstraintBatch.GetConstraintsInvolvingParticle(1).Count != 0 ?
                                                        pinConstraintBatch.GetConstraintsInvolvingParticle(1)[0] : -1;
    }
}
