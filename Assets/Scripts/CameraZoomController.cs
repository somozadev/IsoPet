using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    
    private Camera cam;
    private float targetZoom;
    public float zoomFactor;
    [SerializeField] private float zoomSpeed = 3f; 
    [SerializeField] private float[] limits = new float[2]; 
    private float scrollData;

    public bool isLocked;


    void Start()
    {
        cam = this.GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
        isLocked = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!isLocked)
        {
            
            scrollData = Input.GetAxis("Mouse ScrollWheel");
            targetZoom -= scrollData*zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom,limits[0],limits[1]);
            targetZoom = Mathf.Round(targetZoom);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,targetZoom,Time.deltaTime * zoomSpeed);
    
        }
    }
}
