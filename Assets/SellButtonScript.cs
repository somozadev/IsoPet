using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class SellButtonScript : MonoBehaviour
{
    public TMP_Text price;
    public ShopLogic shopLogic;
    public GameObject sold;
    public RoomsLogic roomsLogic;

    public GameObject economy_ui;
    // public GameObject lockUnlock;
    // public GameObject buttonShop;
    public GameObject shop;

    void Awake()
    {
        // lockUnlock = GameObject.FindWithTag("LockUnlock");
        economy_ui = GameObject.FindWithTag("ECONOMY_UI");
        // buttonShop = GameObject.FindWithTag("ButtonShop");
        shop = GameObject.FindWithTag("SHOP");
        roomsLogic = GameObject.FindWithTag("ROOMS_LOGIC").GetComponent<RoomsLogic>();
        shopLogic = GameObject.FindWithTag("SHOP_LOGIC").GetComponent<ShopLogic>();
        
    }

    public void Sell()
    {
        shopLogic.IncreaseEconomy(Convert.ToInt32(price.text));
        sold.SetActive(true);
        this.GetComponent<Button>().interactable = false;
        foreach(GameObject pet in shopLogic.persistantActualHoldingPets)
        {
            if(this.GetComponentInParent<SellingPetMarker>().gameObject == pet)
            {
                foreach(PetInfo petInfo in FindObjectsOfType<PetInfo>())
                   {
                       Debug.Log("pet: " + petInfo.name);
                       Debug.Log("PREFAB NAME: " + shopLogic.actualHoldingPets[shopLogic.persistantActualHoldingPets.IndexOf(pet)].prefab.name);
                       Debug.Log("Index: " + roomsLogic.roomsGameobjects.IndexOf(pet.GetComponentInParent<RoomLogic>().gameObject));
                    //is the roomslogic.roomsgameobj.indexoftatatatata the one wrong! el problema es que pet no tiene un roomlogic padre
                       if(petInfo.actualPetInfo.mesh == shopLogic.actualHoldingPets[shopLogic.persistantActualHoldingPets.IndexOf(pet)].prefab)
                       {
                           roomsLogic.roomsGameobjects.RemoveAt(roomsLogic.roomsGameobjects.IndexOf(pet.GetComponentInParent<RoomLogic>().gameObject));
                           roomsLogic.i--; //! problemas con esto en cuanto al orden y su colocacion seguro!!!
                           Destroy(pet.GetComponentInParent<RoomLogic>().gameObject);
                       }
                   }
            }
        }

    }
}
