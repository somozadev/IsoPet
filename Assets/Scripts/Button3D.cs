using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button3D : MonoBehaviour
{
    
    public GameObject defineButton;
    public RoomLogic roomLogic;
    public Actions actions;
    public Camera roomCamera;

    void Awake()
    {
        roomLogic = transform.parent.GetComponent<RoomLogic>();
        roomCamera = Camera.main;
    }

    void Start()
    {   
        defineButton = this.gameObject;
    }

    void Update()
    {
        var ray = roomCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;


        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == defineButton)
            {
                   IsoPetEvents.GiveValueToPet.Invoke(new ButtonEventData(gameObject.GetComponent<Animator>(),roomLogic.pet.GetComponent<PetInfo>(),actions));
                   
            }
        }
    }
}
