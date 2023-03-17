using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanelMinigame1 : MonoBehaviour
{
    //public GameObject lever1, lever2, lever3, lever4;
    Vector3 mousePosition;
    public Light indicatorLight;
    public Camera cam;


    private Vector3 GetMousePos()
    {
        return cam.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }
    private void OnMouseDrag()
    {
        Vector3 mouseMovement = cam.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        if (transform.localPosition.y < 0.24 & mouseMovement.y > 1.9)
        {
            transform.position = new Vector3(transform.position.x, mouseMovement.y, transform.position.z);
            indicatorLight.color = Color.red;
            Debug.Log("pos y: " + transform.localPosition.y);
        } 
        else if(transform.localPosition.y > 0.24)
        {
            indicatorLight.color = Color.green;
            Debug.Log("LLEGO A POSICION CORRECTA, pos y: " + transform.localPosition.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Only for test
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Luces fuera");
            turnOffLights();
        }
        if(transform.localPosition.y >= 0.24)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0.25f, transform.localPosition.z);
        }
    }

    public void turnOffLights()
    {
        transform.localPosition = new Vector3(-0.9f, Random.Range(-0.260f,0.125f), transform.localPosition.z);
        indicatorLight.color = Color.red;
    }
}