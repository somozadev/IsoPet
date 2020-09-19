using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet 
{
    public string name; 
    public int hpLvl;
    public int maxLvl;
    public int waterLvl;
    public int foodLvl;
    public int energyLvl;   
    public int happynessLvl; 
    
    public float dayTimeMin;
    public float waterTimeLoss;
    public float foodTimeLoss;
    public float gamesTimeLoss;
    public float sleepTimeLoss;

    public Actions petActions;
    public Animations petAnimations;
    public GameObject mesh;
    public Material originalMaterial;
    public GameObject childMesh;
    public List<GameObject> petParticles;
    public List<Material> petMeshMaterials;
    public List<GameObject> spriteParents;
    public VisualPointsLists pointsLists;

    public Rarity rarity;
    

    public Pet(string _name,int _hpLvl,  int _waterLvl, int _foodLvl, int _energyLvl, int _happynessLvl, int _maxLvl,
    float _dayTimeMin, float _waterTimeLoss, float _foodTimeLoss, float _gamesTimeLoss, float _sleepTimeLoss,
     Actions _petActions, GameObject _mesh, List<GameObject> _spriteParents, VisualPointsLists _pointsList, 
     Animations _petAnimations, List<GameObject> _petParticles, List<Material> _petMeshMaterials, Material _originalMaterial
     , Rarity _rarity)
    {
        name = _name;
        hpLvl = _hpLvl;

        waterLvl = _waterLvl;
        foodLvl = _foodLvl;
        energyLvl = _energyLvl;
        happynessLvl = _happynessLvl;
        maxLvl = _maxLvl;
        
        dayTimeMin = _dayTimeMin;
        waterTimeLoss = _waterTimeLoss;
        foodTimeLoss = _foodTimeLoss;
        gamesTimeLoss = _gamesTimeLoss;
        sleepTimeLoss = _sleepTimeLoss;

        petActions = _petActions;
        petAnimations = _petAnimations;
        mesh = _mesh;
        petParticles = _petParticles;
        spriteParents = _spriteParents;
        pointsLists = _pointsList;
        petMeshMaterials = _petMeshMaterials;
        originalMaterial = _originalMaterial;

        rarity = _rarity;
    
    }
    
  

    //Instantiates the prefab mesh as child of the pet GameObject
    public void Initialze(Transform parent)
    {
        GameObject aux = GameObject.Instantiate(mesh ,new Vector3(0,0.6f,0) ,Quaternion.identity, parent);
        aux.transform.Rotate(new Vector3(0,0,0));
        childMesh = aux;
    }   
    //Updates visualUI of the pet when button clicked (settled in roomlogic within ButtonEvent)
    public void UpdateVisuals(int statLvl, List<GameObject> typeOfList)
    { 
        if(statLvl==maxLvl)
        {
            foreach(GameObject ob in typeOfList)
            {
                ob.SetActive(true);
            }
        }
        if(statLvl==(maxLvl-1))
        {
            for(int i = 0; i < statLvl-1; i++)
                 typeOfList[i].gameObject.SetActive(true);
            typeOfList[statLvl].gameObject.SetActive(false);    
            
        }
        if(statLvl==(maxLvl-2) && maxLvl-2 > 0)
        { 
            for(int i = 0; i < statLvl-2; i++)
                 typeOfList[i].gameObject.SetActive(true);
            typeOfList[statLvl].gameObject.SetActive(false);
        }
        if(statLvl==(maxLvl-3) && maxLvl-3 > 0 )
        {
             for(int i = 0; i < statLvl; i++)
                 typeOfList[i].gameObject.SetActive(true);
            typeOfList[maxLvl].gameObject.SetActive(false);
        }
        if(statLvl==(maxLvl-4) && maxLvl-4 > 0)
        {
             for(int i = 0; i < statLvl; i++)
                 typeOfList[i].gameObject.SetActive(true);
            typeOfList[maxLvl].gameObject.SetActive(false);
        }
        if(statLvl==0)
        {
            foreach(GameObject ob in typeOfList)
            {
                ob.SetActive(false);
            }
        }
         if(statLvl<0)
        {
            //stopMeshUpdateds
        }
    }
    //Instantiates visualUI of the pet based on his pointLvls (water,food,games,sleep)
    public void SetVisuals( List<GameObject> sprites, VisualPointsLists _pointsLists )
    {
        
        int increment = 30;
        for(int i = 0; i < waterLvl; i++)
        {
            GameObject aux = GameObject.Instantiate(sprites[0],new Vector3(increment,0,0),Quaternion.identity,spriteParents[0].transform);
            aux.transform.localPosition = new Vector3(increment, 0, 0);
            aux.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
            increment+=20;
            _pointsLists.waterPoints.Add(aux);
        }
        increment = 30;
        for(int i = 0; i < foodLvl; i++)
        {
            GameObject aux = GameObject.Instantiate(sprites[1],new Vector3(increment,0,0),Quaternion.identity,spriteParents[1].transform);
            aux.transform.localPosition = new Vector3(increment, 0, 0);
            aux.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
            increment+=20;
            _pointsLists.foodPoints.Add(aux);
            
        }
        increment = 30;
        for(int i = 0; i < happynessLvl; i++)
        {
            GameObject aux = GameObject.Instantiate(sprites[2],new Vector3(increment,0,0),Quaternion.identity,spriteParents[2].transform);
            aux.transform.localPosition = new Vector3(increment, 0, 0);
            aux.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
            increment+=20;
            _pointsLists.gamesPoints.Add(aux);
        }
        increment = 30;
        for(int i = 0; i < energyLvl; i++)
        {
            GameObject aux = GameObject.Instantiate(sprites[3],new Vector3(increment,0,0),Quaternion.identity,spriteParents[3].transform);
            aux.transform.localPosition = new Vector3(increment, 0, 0);
            aux.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
            increment+=20;
            _pointsLists.sleepPoints.Add(aux);
        }
    }
    //Called to modify pet pointLvls values, each button triggers diffrent case
    public void PerformAction()
    {
        switch(petActions)
        {
            case Actions.GiveWater:
                if(waterLvl < maxLvl)
                waterLvl+=1;
            break;
            case Actions.GiveFood:
                if(foodLvl < maxLvl)
                foodLvl+=1;
            break;
            case Actions.GiveGames:
                if(happynessLvl < maxLvl)
                happynessLvl+=1;
            break;
            case Actions.GiveSleep:
                if(energyLvl < maxLvl)
                energyLvl+=1;
            break;

            default: return;
        }
    }
    //Changes ChildMesh material from pet object
    public void ChangeMaterial(int index) =>childMesh.GetComponent<MeshRenderer>().material = petMeshMaterials[index];
    //Resets ChildMesh material to original
    public void ResetMaterial() 
    {
        if(childMesh!=null)
        childMesh.GetComponent<MeshRenderer>().material = originalMaterial;
    }
    //Set the new animations option (enum) for the pet 
    public void SetAnimation(Animations option) => this.petAnimations = option;
}





    //Possible actions of the pet 
    public enum Actions
    {
        StayStill,
        GiveWater,
        GiveFood,
        GiveGames,
        GiveSleep,
        Dead
    }
    public enum Animations
    {
        Move,
        Idle,
        Eat,
        Drink,
        Sleep,
        Dead
    }

    

//Class that stores lists of all visualUI elements of the pet 
[System.Serializable]
public class VisualPointsLists
{
    public List<GameObject> waterPoints;
    public List<GameObject> foodPoints;
    public List<GameObject> gamesPoints;
    public List<GameObject> sleepPoints;

    public  VisualPointsLists()
    {   
        waterPoints = new List<GameObject>();
        foodPoints = new List<GameObject>();
        gamesPoints = new List<GameObject>();
        sleepPoints = new List<GameObject>();
    }

}