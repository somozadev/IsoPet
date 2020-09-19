using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PetInfo : MonoBehaviour
{

    public Pet actualPetInfo; 
    public List<GameObject> petPoints;
    public float dayTimePassed;
    public int numberOfLvlsForDie;

    private  float waterTimePassed;
    private  float foodTimePassed;
    private  float gamesTimePassed;
    private  float sleepTimePassed;
    public int dayCount;
    private RoomLogic roomLogic;
    private ShopLogic shopLogic;
    

    void Awake()
    {
        roomLogic = transform.parent.GetComponent<RoomLogic>();
        shopLogic = GameObject.FindWithTag("SHOP_LOGIC").GetComponent<ShopLogic>();
    }

    void Start()
    {
        numberOfLvlsForDie = Random.Range(1,4);
        actualPetInfo.Initialze(transform);
        actualPetInfo.SetVisuals(petPoints, actualPetInfo.pointsLists);
    }
    
    void Update()
    {
        if(actualPetInfo.hpLvl>=0)
        {
            
            dayTimePassed += Time.deltaTime;

            waterTimePassed += Time.deltaTime;
            foodTimePassed += Time.deltaTime;
            gamesTimePassed += Time.deltaTime;
            sleepTimePassed += Time.deltaTime;

            if(waterTimePassed >= actualPetInfo.waterTimeLoss*60 && actualPetInfo.waterLvl >= 0 )
            {
                actualPetInfo.waterLvl--; 
                waterTimePassed=0;        
                actualPetInfo.UpdateVisuals(actualPetInfo.waterLvl, actualPetInfo.pointsLists.waterPoints);
                actualPetInfo.ChangeMaterial(0);
                ShakeCam();
                StartCoroutine(resetMat());
                
            }
            if(foodTimePassed >= actualPetInfo.foodTimeLoss*60 && actualPetInfo.foodLvl >= 0 )
            {
                actualPetInfo.foodLvl--;   
                foodTimePassed=0;
                actualPetInfo.UpdateVisuals(actualPetInfo.foodLvl, actualPetInfo.pointsLists.foodPoints);
                actualPetInfo.ChangeMaterial(1);
                ShakeCam();
                StartCoroutine(resetMat());
            }
            if(gamesTimePassed >= actualPetInfo.gamesTimeLoss*60 && actualPetInfo.happynessLvl >= 0 )
            {
                actualPetInfo.happynessLvl--;   
                gamesTimePassed=0;
                actualPetInfo.UpdateVisuals(actualPetInfo.happynessLvl, actualPetInfo.pointsLists.gamesPoints);
                actualPetInfo.ChangeMaterial(2);
                ShakeCam();
                StartCoroutine(resetMat());
            }
            if(sleepTimePassed >= actualPetInfo.sleepTimeLoss*60 && actualPetInfo.energyLvl >= 0 )
            {
                actualPetInfo.energyLvl--;   
                sleepTimePassed=0;
                actualPetInfo.UpdateVisuals(actualPetInfo.energyLvl, actualPetInfo.pointsLists.sleepPoints);
                actualPetInfo.ChangeMaterial(3);
                ShakeCam();
                 StartCoroutine(resetMat());
            }
            
            if(dayTimePassed >= actualPetInfo.dayTimeMin * 60)
            {
                dayCount++;
                dayTimePassed=0;
                roomLogic.UpdateDayCount(dayCount);
                shopLogic.IncreaseEconomy(dayCount * 100);
                shopLogic.totalDays++;
                
            }
            CheckHp();
        }
    }
    public void InstantiateParticle(Actions action)
    {
        int index=0;
        switch(action)
        {
            case Actions.GiveWater:
            index = 0;
            break;
            case Actions.GiveFood:
            index = 1;
            break;
            case Actions.GiveGames:
            index = 2;
            break;
            case Actions.GiveSleep:
            index = 3;
            break;
            case Actions.Dead:
            index = 4;
            break;
        }
        GameObject particle;
            if(index!=4)
            {
                particle = Instantiate(actualPetInfo.petParticles[index],new Vector3(0,0.5f,0), Quaternion.identity, roomLogic.transform);
                
            }
            else
            { 
                particle = Instantiate(actualPetInfo.petParticles[index],transform.position, Quaternion.identity, transform);
            }
               

            particle.transform.position = transform.position;
            particle.transform.rotation = (Quaternion.Euler(new Vector3(270,0,0)));
            Destroy(particle,5);
    }
    public void SetPetInfo(Pet pet) => actualPetInfo = pet;
    private void CheckHp()
    {
        if(LvlsToZeroForDie(numberOfLvlsForDie))
            actualPetInfo.hpLvl--;
    }
    bool LvlsToZeroForDie(int amount)
    {
        bool result=false;
        switch(amount)
        {
            case 1:
                if(actualPetInfo.waterLvl <= 0 || actualPetInfo.foodLvl <= 0 || actualPetInfo.happynessLvl <= 0 || actualPetInfo.energyLvl <= 0 )
                    result = true;
                else result=false;
                break;
            case 2:
                if((actualPetInfo.waterLvl <= 0 && actualPetInfo.foodLvl <= 0) ||
                 (actualPetInfo.waterLvl <= 0 && actualPetInfo.happynessLvl <= 0) ||
                 (actualPetInfo.waterLvl <= 0 && actualPetInfo.energyLvl <= 0) ||
                 (actualPetInfo.foodLvl <= 0 && actualPetInfo.happynessLvl <= 0)||
                 (actualPetInfo.foodLvl <= 0 && actualPetInfo.energyLvl <= 0)||
                 (actualPetInfo.happynessLvl <= 0 && actualPetInfo.energyLvl <= 0))
                    result = true;
                else result=false;
                break;
            case 3:
                if((actualPetInfo.waterLvl <= 0 && actualPetInfo.foodLvl <= 0 && actualPetInfo.happynessLvl <= 0) ||
                 (actualPetInfo.waterLvl <= 0 && actualPetInfo.foodLvl <= 0 && actualPetInfo.energyLvl <= 0) ||
                 (actualPetInfo.waterLvl <= 0 && actualPetInfo.happynessLvl <= 0 && actualPetInfo.energyLvl <= 0) ||
                 (actualPetInfo.foodLvl <= 0 && actualPetInfo.happynessLvl <= 0 && actualPetInfo.energyLvl <= 0))
                    result = true;
                else result=false;
                break;
            case 4: 
                if(actualPetInfo.waterLvl <= 0 && actualPetInfo.foodLvl <= 0 && actualPetInfo.happynessLvl <= 0 && actualPetInfo.energyLvl <= 0 )
                    result = true;
                else result=false;
                break;
        }
        return result;
    } 
    IEnumerator resetMat()
    {
        yield return new WaitForSeconds(0.70f);
        actualPetInfo.ResetMaterial();
    }
    void ShakeCam()
    {
        Camera.main.GetComponentInParent<Animator>().SetTrigger("Shake");
    }
}


