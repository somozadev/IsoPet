using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public GameObject buyButtonContent;
    public GameObject sellButtonContent;
    public GameObject randomButtonContent;
    public GameObject mixButtonContent;
    
    public ShopLogic shopLogic;

    void Start()
    {
        shopLogic = GameObject.FindWithTag("SHOP_LOGIC").GetComponent<ShopLogic>();
    }

    public void BuyButtonClick()
    {
            buyButtonContent.SetActive(true);
            sellButtonContent.SetActive(false);
            randomButtonContent.SetActive(false);
            mixButtonContent.SetActive(false); 
            shopLogic.uNeedToHavePetsFirst.SetActive(false);
    } 
    public void SellButtonClick()
    {
            buyButtonContent.SetActive(false);
            sellButtonContent.SetActive(true);
            randomButtonContent.SetActive(false);
            mixButtonContent.SetActive(false);
    }
    public void RandomButtonClick()
    {
            buyButtonContent.SetActive(false);
            sellButtonContent.SetActive(false);
            randomButtonContent.SetActive(true);
            mixButtonContent.SetActive(false);
            shopLogic.uNeedToHavePetsFirst.SetActive(false);
    }
    public void MixButtonClick()
    {  
            buyButtonContent.SetActive(false);
            sellButtonContent.SetActive(false);
            randomButtonContent.SetActive(false);
            mixButtonContent.SetActive(true);      
            shopLogic.uNeedToHavePetsFirst.SetActive(false);  
    }

}
