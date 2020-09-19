using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomLogic : MonoBehaviour
{
    public Pet petToSet;
    // public bool isRandom;
    public Rarity rarity;
    public GameObject boughtPetMesh;
    public GameObject pet; 
    public GameObject petName;
    public GameObject dayCount;

    public string roomName;
    public List<GameObject> petParticles;
    public Material originalMaterial;
    public List<Material> petMeshMaterials;
    public List<GameObject> spriteParents;
    public List<GameObject> petMeshes;
    VisualPointsLists pointLists;

    private GameObject aux;

    void Awake() 
    {
        //this.transform.position = GetComponentInParent<RoomsLogic>().gridPositions[GetComponentInParent<RoomsLogic>().count];
        
        rarity = GetComponentInParent<RoomsLogic>().rarity;
        boughtPetMesh = GetComponentInParent<RoomsLogic>().mesh;


        pointLists = new VisualPointsLists();
        // instantiates the petname ui
        aux =GameObject.Instantiate(petName, Vector3.zero, Quaternion.identity,gameObject.GetComponentInChildren<Canvas>().gameObject.transform);
        auxSettler(aux);
        // instantiates the daycount ui
        GameObject aux2 = GameObject.Instantiate(dayCount,Vector3.zero,Quaternion.identity,gameObject.GetComponentInChildren<Canvas>().gameObject.transform);
        dayCount = aux2;
        auxSettler2(dayCount);


        foreach(GameObject sprite in spriteParents)
        {
            sprite.SetActive(true);
        }
        foreach(TMP_Text obj in petName.GetComponentsInChildren<TMP_Text>())
        {
            if(petToSet.name.Length <= 13)
                obj.text = petToSet.name;
            else
            {
                Debug.LogWarning("Pet name:" + petToSet.name + ". Is too long");
            }
        }
        IsoPetEvents.GiveValueToPet.AddListener(GivePet);

         GetPets();
    }
    void Start()
    {
         foreach(TextMeshProUGUI childs in aux.GetComponentsInChildren<TextMeshProUGUI>())
        {
            childs.text = petToSet.name;
        }
    
        //warning when selling pet
         
        transform.position = GetComponentInParent<RoomsLogic>().gridPositions[GetComponentInParent<RoomsLogic>().i];
        GetComponentInParent<RoomsLogic>().i++;
    }
    
    public void UpdateDayCount(int day)
    {
        dayCount.GetComponent<TMP_Text>().text = "Day: " + day; 
    }
    public void GetPets()
    {
       roomName = this.name;
       foreach(Transform child in transform)
       {
           if(child.GetComponent<PetInfo>())
           {
               pet = child.gameObject;
           }
       }
      
            CreateBoughtPet(pet,boughtPetMesh);
    }
    
    public void CreateBoughtPet(GameObject pet, GameObject meshPrefab)
    {
        int max = Random.Range(2,5);
        int dayt = Random.Range(5,15);
        originalMaterial = meshPrefab.GetComponent<Renderer>().sharedMaterial;
        petToSet = new Pet(petToSet.name,5, max,max,max,max,max,dayt,dayt/5,dayt/3, dayt ,
        dayt/dayt,Actions.StayStill, meshPrefab,spriteParents,pointLists,
        Animations.Idle,petParticles,petMeshMaterials,originalMaterial,rarity);
        pet.GetComponent<PetInfo>().SetPetInfo(petToSet);
        GetComponentInChildren<Grid>().passedRarity = rarity;
    }
    // public void CreateRandomPet(GameObject pet)
    // {
    //     int max = Random.Range(2,5);
    //     int dayt = Random.Range(5,15);
    //     petToSet = new Pet(petToSet.name,5, max,max,max,max,max,dayt,dayt/5,dayt/3, dayt ,
    //     dayt/dayt,Actions.StayStill, petMeshes[Random.Range(0,petMeshes.Count)],
    //     spriteParents,pointLists,Animations.Idle,petParticles,petMeshMaterials,originalMaterial);
    //     pet.GetComponent<PetInfo>().SetPetInfo(petToSet);
    // }
    public void GivePet(ButtonEventData data)
    { 
        data.buttonAnimator.SetTrigger("OnClick");
        data.petTarget.actualPetInfo.petActions = data.action;
        data.petTarget.actualPetInfo.PerformAction();

        data.petTarget.actualPetInfo.UpdateVisuals(data.petTarget.actualPetInfo.waterLvl, data.petTarget.actualPetInfo.pointsLists.waterPoints);
        data.petTarget.actualPetInfo.UpdateVisuals(data.petTarget.actualPetInfo.foodLvl, data.petTarget.actualPetInfo.pointsLists.foodPoints);
        data.petTarget.actualPetInfo.UpdateVisuals(data.petTarget.actualPetInfo.happynessLvl, data.petTarget.actualPetInfo.pointsLists.gamesPoints);
        data.petTarget.actualPetInfo.UpdateVisuals(data.petTarget.actualPetInfo.energyLvl, data.petTarget.actualPetInfo.pointsLists.sleepPoints); 
        data.petTarget.InstantiateParticle(data.petTarget.actualPetInfo.petActions);
   
   
    }
    void auxSettler(GameObject _aux)
    {
        _aux.GetComponent<RectTransform>().localPosition = Vector3.zero;
        _aux.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
        _aux.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        _aux.GetComponent<RectTransform>().localScale = Vector3.one;
    }
    void auxSettler2(GameObject _aux)
    {
        _aux.transform.position = new Vector3(-190,-150,0);
        _aux.GetComponent<RectTransform>().localPosition = new Vector3(-190,-150,0);
        _aux.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
        _aux.GetComponent<RectTransform>().localScale = Vector3.one;
    }
    
}
