using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopLogic : MonoBehaviour
{
    
    [Header("Stats")]   
    public int totalDays;
    public float playerEconomy;

    [Header("Economy Info UI")]    
    public GameObject economyUI;
    public GameObject economyUiIcon;
    public TMP_Text economy,economyBackground;


    [Header("Needed References")]   
    public RoomsLogic roomsLogic;
    public GameObject shopBackground;
    public GameObject parentCanvas;
    public GameObject nameSetPrefab;


    [Header("Buy")]   
    public GameObject shopItemPrefab;
    public GameObject shopItemPrefabPARENT;
    public GameObject startNameSetPrefab;

    public ShopItemsSell iconsShop; 
    public List<ItemRarity> actualSellingPets;
    public List<GameObject> actualSellingPetsGameObject;
    public List<GameObject> actualSoldPets;
    

    [Header("Sell")]   
    public List<ItemRarity> actualHoldingPets;
    public List<GameObject> persistantActualHoldingPets;
    public List<GameObject> nonPersistantActualHoldingPets;
    public GameObject shopItemSellPrefab;
    public GameObject shopItemSellPrefabPARENT;
    public GameObject uNeedToHavePetsFirst;
    


    void Start()
    {
        actualSoldPets = new List<GameObject>();
        actualSellingPets = new List<ItemRarity>();
        actualHoldingPets = new List<ItemRarity>();
        actualSellingPetsGameObject = new List<GameObject>();
        nonPersistantActualHoldingPets = new List<GameObject>();
        persistantActualHoldingPets = new List<GameObject>();
        StartShop();
        UpdateEconomy();
    }

    public void IncreaseEconomy(float income)
    {
        playerEconomy+=income;
        UpdateEconomy();
    }
    public void DecreaseEconomy(float outcome)
    {
        playerEconomy-=outcome;
        UpdateEconomy();
    }
    void UpdateEconomy()
    {
        economy.text = playerEconomy.ToString("000000000");    
        economyBackground.text = playerEconomy.ToString("000000000");    
    }


    //On Open/Close Shop -> hold tab record!!
    public void OpenShop(GameObject shop) 
    {
        economyUI.GetComponent<RectTransform>().localPosition = new Vector3(-285,-85,0);
        economyUiIcon.GetComponent<RectTransform>().localScale = new Vector3(0.7f,0.7f,1);
        economyUiIcon.GetComponent<RectTransform>().localPosition = new Vector3(480,410,0);
        shop.SetActive(true);
        Camera.main.GetComponent<CameraZoomController>().isLocked = true;
        Camera.main.GetComponent<CameraMoveController>().locked = true;
        Camera.main.GetComponent<CameraMoveController>().lockUnlock.GetComponent<Image>().sprite = Camera.main.GetComponent<CameraMoveController>().lockers[1];
        CheckPricesAndMoney(actualSellingPets,actualSellingPetsGameObject);
    }
    public void CloseShop(GameObject shop)
    {
        uNeedToHavePetsFirst.SetActive(false);
        economyUI.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
        economyUiIcon.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        economyUiIcon.GetComponent<RectTransform>().localPosition = new Vector3(435,410,0);
        shop.SetActive(false);
        Camera.main.GetComponent<CameraZoomController>().isLocked = false;
    }
    public void HideUIPiece(GameObject piece) => piece.SetActive(false);
    public void ShowUIPiece(GameObject piece) => piece.SetActive(true);

    public void InstanciatePetName()
    {
        GameObject aux = Instantiate(nameSetPrefab, Vector3.zero,Quaternion.identity,parentCanvas.transform);
        aux.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
    }
    public void CheckPricesAndMoney(List<ItemRarity> lista, List<GameObject> listaGameobject)
    {
        int i = 0;

        foreach(ItemRarity item in lista)
        {
            
            if(!ComparePrices(item.price))
               {
                   var colors = listaGameobject[i].GetComponentInChildren<Button>().colors;
                   colors.normalColor = Color.red;
                   listaGameobject[i].GetComponentInChildren<Button>().colors = colors;
                   listaGameobject[i].GetComponentInChildren<Button>().interactable = false;
               }
               else    
               {
                   var colors = listaGameobject[i].GetComponentInChildren<Button>().colors;
                   colors.normalColor = Color.green;
                   listaGameobject[i].GetComponentInChildren<Button>().colors = colors;
                   listaGameobject[i].GetComponentInChildren<Button>().interactable = true;
               }
            i++;
        }
        
    }
    bool ComparePrices(float toCompare)
    {
        if(toCompare > playerEconomy)
            return false;
        else
            return true;
    }

    //BUG ENCONTRADO EN UNITY -> esperar a unity lo fixee o usar cuatro imagentes distintas y .overrideSprite
    public void ChooseBackgroundColour(int colour)
    {
        switch(colour)
        {
            case 1:
            shopBackground.GetComponent<Image>().color = new Color(209,255,163);
            break;
            case 2:
            shopBackground.GetComponent<Image>().color = new Color(255,175,175);
            break;
            case 3:
            shopBackground.GetComponent<Image>().color = new Color(228,172,255);
            break;
            case 4:
            shopBackground.GetComponent<Image>().color = new Color(255,163,231);
            break;
            
        }
    }
  
    public void OpenSellShop() //PROBLEMAS CADA VEZ QUE ABRES SELL GAP DUPLICAN LISTAS 
    {
        // if(nonPersistantActualHoldingPets.Count!=actualSoldPets.Count)
        // {
            // foreach(GameObject item in nonPersistantActualHoldingPets)
            // {
            //     nonPersistantActualHoldingPets.Remove(item);
            //     persistantActualHoldingPets.Remove(item);
            //     Destroy(item);
            // }
        // }
        nonPersistantActualHoldingPets = new List<GameObject>();
        persistantActualHoldingPets = new List<GameObject>();

        //hacer otra funcion para borrar todas las instancias (y sus posiciones de las listas) y llamarla al darle a los buttns de arriba o la X 

        if(actualHoldingPets.Count == 0)
        {
            uNeedToHavePetsFirst.SetActive(true);
        }
        else
            uNeedToHavePetsFirst.SetActive(false);

        foreach(ItemRarity item in actualHoldingPets)
        {
            GameObject aux = Instantiate(shopItemSellPrefab,Vector3.zero,Quaternion.identity,shopItemSellPrefabPARENT.transform);
            aux.GetComponent<Image>().sprite = item.bordersShop;
            //Check if the pet is dead or not here! 
            
            aux.GetComponentInChildren<TMP_Text>().text = (item.price * 0.75f).ToString();
            aux.GetComponentInChildren<ChildIconMarker>().GetComponent<Image>().sprite = item.icon;
            nonPersistantActualHoldingPets.Add(aux);
            persistantActualHoldingPets.Add(aux);
        }   
    }
    public void StartShop()
    {
        foreach(ItemRarity item in iconsShop)
        {
            GameObject aux =Instantiate(shopItemPrefab,Vector3.zero,Quaternion.identity,shopItemPrefabPARENT.transform);
            aux.GetComponent<Image>().sprite = item.bordersShop;
            aux.GetComponentInChildren<TMP_Text>().text = item.price.ToString();
            aux.GetComponentInChildren<ChildIconMarker>().GetComponent<Image>().sprite = item.icon;
            if(!ComparePrices(item.price))
            {
                var colors = aux.GetComponentInChildren<Button>().colors;
                colors.normalColor = Color.red;
                aux.GetComponentInChildren<Button>().colors = colors;
                aux.GetComponentInChildren<Button>().interactable = false;
            }
            else    
            {
                var colors = aux.GetComponentInChildren<Button>().colors;
                colors.normalColor = Color.green;
                aux.GetComponentInChildren<Button>().colors = colors;
                aux.GetComponentInChildren<Button>().interactable = true;
            }
            if(item.rarity.Equals(Rarity.Gold))
                aux.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        //lista de Itemraritys
        actualSellingPets.Add(item); 
        //lista de los gameobjects asociados
        actualSellingPetsGameObject.Add(aux);
        }
    }


}
