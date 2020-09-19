using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{   
    public List<GameObject> buttons;
    public int width,height;
    public GameObject cellPrefab;
    public List <GameObject> cells;
    int [,] grid; 
    public Rarity passedRarity;
    public List<Texture> rarityTextures; 

void Awake()
{
     grid = new int[width,height];
     StartCoroutine(GenerateGrid());
     ChangeGridColor(passedRarity);
}   

    public IEnumerator GenerateGrid()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        
        
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j=0; j < grid.GetLength(1); j++)
            {
                GameObject inst = Instantiate(cellPrefab,Vector3.zero,Quaternion.identity,this.gameObject.transform);
                inst.transform.localPosition =  new Vector3(i,0,j) * cellPrefab.transform.localScale.x;
                cells.Add(inst);
            }   
        }

    Vector3 added = new Vector3(-1,0,0.2f);
    for(int i = 0; i < buttons.Count; i++)
    {
        GameObject inst = Instantiate(buttons[i], added, Quaternion.identity, gameObject.transform.parent );
        added.z += 1.5f;
    }
        yield return wait;
        
    }

    public void ChangeGridColor(Rarity rarity)
    {
        int i = 0;
        bool emiss = false;
        switch(rarity)
        {
            case Rarity.Blue:
                i = 0;
            break;
            case Rarity.Green:
                i = 1;
            break;
            case Rarity.Purple:
                i = 2;
            break;
            case Rarity.Gold:
                i = 3;
                emiss = true;
            break;
        }
        foreach(GameObject cell in cells)
        {
            cell.GetComponent<Renderer>().material.mainTexture =  rarityTextures[i];
            if(emiss)
            {
                cell.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                cell.GetComponent<Renderer>().material.SetColor("_EMISSION",Color.yellow);
            }
        }
        emiss = false;
    }

   

}
