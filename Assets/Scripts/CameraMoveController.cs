using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveController : MonoBehaviour
{
    public float borderX = 10;
    public float borderY = 5.625f;
    public float cameraSpeed = 3;
    public bool locked;
    public Sprite [] lockers  = new Sprite[2];
    public float ScreenHeigh,ScreenWidth;
    public Vector3 mousepos;
    public GameObject lockUnlock;
    void Start()
    {
        lockUnlock = GameObject.FindWithTag("LockUnlock");
    }
    void Update()
    {
        mousepos = Input.mousePosition;
        ScreenHeigh = Screen.height;
        ScreenWidth = Screen.width;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Lock_Unlock(lockUnlock);
        }


        if(!locked)
        {
                if(Input.mousePosition.x >= Screen.width - borderX)
            {
                transform.localPosition += new Vector3(1,0,-0) * Time.deltaTime * cameraSpeed;
                //transform.localPosition = Vector3.Lerp(transform.localPosition,transform.localPosition +  new Vector3(1,0,-1),Time.deltaTime * cameraSpeed );
            }
            else if(Input.mousePosition.x <= borderX)
            {
                transform.localPosition += new Vector3(-1,0,0)* Time.deltaTime * cameraSpeed;
            } 
            else if(Input.mousePosition.y >= Screen.height - borderY)
            {
                transform.localPosition +=  new Vector3(0,2,0) * Time.deltaTime * cameraSpeed;
            }
            else if(Input.mousePosition.y <= borderY)
            {
                transform.localPosition += new Vector3(-0,-2,-0) * Time.deltaTime * cameraSpeed;
            }
        }       
    }

    public void Lock_Unlock(GameObject button)
    {
        if(locked)
        {
            locked = false;
            button.GetComponent<Image>().sprite = lockers[0];
        }
        else
        {
            locked = true;
            button.GetComponent<Image>().sprite = lockers[1];
        }
    }
}
