using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BuyButtonScript : MonoBehaviour
{
    public TMP_Text price;
    public ShopLogic shopLogic;
    public GameObject soldOut;
    private RoomsLogic roomsLogic;
    public GameObject asociatedMesh;

    public GameObject economy_ui;
    public GameObject lockUnlock;
    public GameObject buttonShop;
    public GameObject shop;

    void Awake()
    {
        lockUnlock = GameObject.FindWithTag("LockUnlock");
        economy_ui = GameObject.FindWithTag("ECONOMY_UI");
        buttonShop = GameObject.FindWithTag("ButtonShop");
        shop = GameObject.FindWithTag("SHOP");
        roomsLogic = GameObject.FindWithTag("ROOMS_LOGIC").GetComponent<RoomsLogic>();
        shopLogic = GameObject.FindWithTag("SHOP_LOGIC").GetComponent<ShopLogic>();
        
    }
    public void Buy()
    {

        
        shopLogic.DecreaseEconomy(Convert.ToInt32(price.text));
        soldOut.SetActive(true);
        this.GetComponent<Button>().interactable = false;
        foreach(GameObject pet in shopLogic.actualSellingPetsGameObject)
        {   
            if(this.GetComponentInParent<SellingPetMarker>().gameObject == pet)
               {
                   asociatedMesh = shopLogic.actualSellingPets[shopLogic.actualSellingPetsGameObject.IndexOf(pet)].prefab; 
                   roomsLogic.SetNewMesh(asociatedMesh); 
                   roomsLogic.SetNewRarity(shopLogic.actualSellingPets[shopLogic.actualSellingPetsGameObject.IndexOf(pet)].rarity);
                   shopLogic.InstanciatePetName(); 
                   shopLogic.actualHoldingPets.Add(shopLogic.actualSellingPets[shopLogic.actualSellingPetsGameObject.IndexOf(pet)]);    
                   shopLogic.actualSellingPets.RemoveAt(shopLogic.actualSellingPetsGameObject.IndexOf(pet));          
                   shopLogic.actualSellingPetsGameObject.Remove(pet);
                   break;
                }

        }
     
        shopLogic.actualSoldPets = roomsLogic.roomsGameobjects;
        shopLogic.ShowUIPiece(buttonShop);
        shopLogic.ShowUIPiece(economy_ui);
        shopLogic.ShowUIPiece(lockUnlock);
        shopLogic.CloseShop(shop);

    } 

}
