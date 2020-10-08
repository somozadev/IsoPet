using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class SellButtonScript : MonoBehaviour
{
    public int index;

    public TMP_Text price;
    public ShopLogic shopLogic;
    public GameObject sold;
    public RoomsLogic roomsLogic;

    public GameObject economy_ui;
    public GameObject lockUnlock;
    public GameObject buttonShop;
    public GameObject shop;

    public GameObject PARENT;

    void Awake()
    {
        index = -1;
        PARENT = transform.parent.gameObject;
        lockUnlock = GameObject.FindWithTag("LockUnlock");
        economy_ui = GameObject.FindWithTag("ECONOMY_UI");
        buttonShop = GameObject.FindWithTag("ButtonShop");
        shop = GameObject.FindWithTag("SHOP");
        roomsLogic = GameObject.FindWithTag("ROOMS_LOGIC").GetComponent<RoomsLogic>();
        shopLogic = GameObject.FindWithTag("SHOP_LOGIC").GetComponent<ShopLogic>();
        
    }
    private void Start()
    {
        foreach (GameObject ob in shopLogic.actualHoldingPetsGameObject)
        {
            if (ob == transform.parent.gameObject)
                index = shopLogic.actualHoldingPetsGameObject.IndexOf(ob);
        }
    }
    public void Sell() //2 DO HERE : BASE ECONOMY INCREASE ON DAY PASSED && IF PET IS DEAD SELL FOR 1$
    {
        
        shopLogic.IncreaseEconomy(Convert.ToInt32(price.text));
        sold.SetActive(true);
        this.GetComponent<Button>().interactable = false;

        


        GameObject aux = shopLogic.actualSoldPets[index].gameObject;

        Debug.Log("ListCount: " + shopLogic.actualSoldPets.Count);
        //update shops positions : 
        for (int i = 0; i < roomsLogic.roomsGameobjects.Count; i++)
        {
            
            roomsLogic.roomsGameobjects[i].transform.position = roomsLogic.gridPositions[i];
        }
        roomsLogic.i--;
        shopLogic.actualSoldPets.RemoveAt(index);
        shopLogic.actualHoldingPets.RemoveAt(index);
        Destroy(aux);
        shop.GetComponent<ShopButtons>().BuyButtonClick();
        shopLogic.ShowUIPiece(buttonShop);
        shopLogic.ShowUIPiece(economy_ui);
        shopLogic.ShowUIPiece(lockUnlock);
        shopLogic.CloseShop(shop);
        shopLogic.ResetSellGameObjectList();
        //
    }
}
