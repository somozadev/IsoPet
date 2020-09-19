using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMeshPrefab : MonoBehaviour
{
    
    public PetInfo parentPet;
    void Start()
    {
        parentPet = GetComponentInParent<PetInfo>();
        
    }

    void Update()
    {
        switch(parentPet.actualPetInfo.petAnimations)
        {
            case Animations.Move:
                GetComponent<Animator>().SetBool("Move",true);
                break;
            case Animations.Idle:
                GetComponent<Animator>().SetBool("Move",false);
                break;
            case Animations.Dead:
                GetComponent<Animator>().SetTrigger("isDead");
                
                break;
        }        
    }
}
