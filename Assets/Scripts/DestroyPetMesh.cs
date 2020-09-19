using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPetMesh : StateMachineBehaviour
{    
    public GameObject tombstone;
    public GameObject particle;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
        
        animator.GetComponentInParent<PetInfo>().InstantiateParticle(Actions.Dead);
        Instantiate(tombstone,animator.GetComponentInParent<PetMovement>().transform.position,Quaternion.Euler(-90,0,0),animator.GetComponentInParent<PetInfo>().transform);
    }
}
