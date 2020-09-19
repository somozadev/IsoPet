using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public float speed = 3;
    public float rotationSpeed = 10;
    [SerializeField] private float timeSinceMoved;
    public Vector3 randomPoint;
    private Pet pet;


    void Start()
    {
        SetRandomPt();
        pet = this.GetComponent<PetInfo>().actualPetInfo;
    }

    void Update()
    {
        if(pet.hpLvl>=0)
        {
            timeSinceMoved += Time.deltaTime;
            if(timeSinceMoved >= Random.Range(5,20))
            {
                if(Vector3Int.RoundToInt(transform.localPosition) != Vector3Int.RoundToInt(randomPoint))
                {
                    MoveIt();
                }
                else
                {
                    timeSinceMoved = 0;
                    SetRandomPt();
                    pet.SetAnimation(Animations.Idle);
                    transform.localPosition = transform.localPosition;
                }
            }
        }
        
        if(pet.hpLvl<=0)
        {
            pet.SetAnimation(Animations.Dead);

        }
    }

    void SetRandomPt() => randomPoint = GetRandomPointInRange(transform.localPosition.y);
            
    
   
    void MoveIt()
    { 
        pet.SetAnimation(Animations.Move);
        this.transform.localPosition = Vector3.Lerp(transform.localPosition,randomPoint,Time.deltaTime * speed );   
        Quaternion targetRotation = Quaternion.LookRotation(randomPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
    }
    Vector3 GetRandomPointInRange(float y)
    {   
            Vector2 aux = Random.insideUnitCircle * 4.5f;
            return new Vector3(Mathf.Abs(aux.x), y ,Mathf.Abs(aux.y));
    }
}
