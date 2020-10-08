using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    public static void ClearChildren(Transform transform)
    {
        GameObject[] childrens = new GameObject[transform.childCount];
        int i = 0;
        foreach(Transform child in transform)
        {
            childrens[i] = child.gameObject;
            i++;
        }
        foreach (GameObject child in childrens)
            GameObject.Destroy(child);
        
    }

}
