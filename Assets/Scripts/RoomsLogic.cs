using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomsLogic : MonoBehaviour
{
    
    public List<RoomLogic> rooms;
    public List<GameObject> roomsGameobjects;
    public GameObject mesh;
    public Rarity rarity;
    public ShopLogic shop;


    public int l;    
    public int i;

    
    public List<Vector3> gridPositions;
    
    
    void Start()
    {
        roomsGameobjects = new List<GameObject>();
        gridPositions = new List<Vector3>();
        i=0;
        CreateRoomsGrid();
    }

    //INDEX OUT OF RANGE??????? SOLVEEE (before was working)

    public void SetRoom(TMP_InputField editedName)
    {
        
        RoomLogic room = GameObject.Instantiate(rooms[0], Vector3.zero, Quaternion.identity, GameObject.FindWithTag("ROOMS_LOGIC").transform);
        roomsGameobjects.Add(room.gameObject);
        room.petToSet.name = editedName.text;
        editedName.gameObject.SetActive(false);    

        
    }
    
    void CreateRoomsGrid()
    {
        l = Mathf.RoundToInt(Mathf.Sqrt(shop.iconsShop.itemsRarity.Count));
        int xPos = 0,zPos = 0;
        for(int x = 0; x < l ; x++)
        {
            for(int y = 0; y < l ; y++)
            {
                gridPositions.Add(new Vector3(xPos,0,zPos));
                xPos+=20;
            }
            xPos = 0;
            zPos+=20;
        }
    }

    
    public void SetNewMesh(GameObject _mesh)=>  mesh = _mesh;
    public void SetNewRarity(Rarity _rarity)=>  rarity = _rarity;
   
}

/*
#region newNotWorking

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomsLogic : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject mesh;
    public Rarity rarity;
    public ShopLogic shop;

    

    public int l;    
    public int count;
    public List<Vector3> gridPositions;
    

    public void SetNewMesh(GameObject _mesh)=>  mesh = _mesh;
    public void SetNewRarity(Rarity _rarity)=>  rarity = _rarity;
    
    void Start() 
    {
        l=0; 
        Debug.Log("l=" + Mathf.RoundToInt(Mathf.Sqrt(shop.iconsShop.items.Count)));
        l = Mathf.RoundToInt(Mathf.Sqrt(shop.iconsShop.items.Count));
        gridPositions = new List<Vector3>();
        CreateRoomsGrid();

        foreach(Vector3 pos in gridPositions)
            Debug.Log(pos);

    }
    public void SetRoom(TMP_InputField editedName)
    {
       GameObject room = GameObject.Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.FindWithTag("ROOMS_LOGIC").transform); //bc cant instantiate w/ a persistent parent object
       room.GetComponent<RoomLogic>().petToSet.name = editedName.text;
       editedName.gameObject.SetActive(false);    
    }
    public void IncrementI()
    {
         count+=1;
         Debug.Log(count);
    }

    public void CreateRoomsGrid()
     {
         int xPos = 0,zPos = 0;
         for(int x = 0; x < l ; x++)
         {
             for(int y = 0; y < l ; y++)
             {
                 gridPositions.Add(new Vector3(xPos,0,zPos));
                 xPos+=20;
             }
             xPos = 0;
             zPos+=20;
         }
     }

   
}
#endregion
*/