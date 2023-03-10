using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanelMinigame1 : MonoBehaviour
{
    //public GameObject lever1, lever2, lever3, lever4;
    Vector3 mousePosition;
    public Light indicatorLight;


    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }
    private void OnMouseDrag()
    {
        Vector3 mouseMovement = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        if (mouseMovement.y < 2.4 & mouseMovement.y > 1.9)
        {
            transform.position = new Vector3(transform.position.x, mouseMovement.y, transform.position.z);
            indicatorLight.color = Color.red;

        } 
        else if(mouseMovement.y > 2.4)
        {
            indicatorLight.color = Color.green;
            Debug.Log("LLEGO A POSICION CORRECTA, pos y: " + mouseMovement.y);
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
        // 
        /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Lever 1")
                {
                    if(lever1.transform.localPosition.y < 0.260)
                    {
                        lever1.transform.localPosition = new Vector3(-0.9f, lever1.transform.localPosition.y + 0.015f, 0.344f);
                    }
                } 
                else if (hit.transform.name == "Lever 2")
                {
                    if (lever2.transform.localPosition.y < 0.260)
                    {
                        lever2.transform.localPosition = new Vector3(-0.9f, lever2.transform.localPosition.y + 0.015f, 0.141f);
                    }
                }
                else if (hit.transform.name == "Lever 3")
                {
                    if (lever3.transform.localPosition.y < 0.260)
                    {
                        lever3.transform.localPosition = new Vector3(-0.9f, lever3.transform.localPosition.y + 0.015f, -0.141f);
                    }
                }
                else if (hit.transform.name == "Lever 4")
                {
                    if (lever4.transform.localPosition.y < 0.260)
                    {
                        lever4.transform.localPosition = new Vector3(-0.9f, lever4.transform.localPosition.y + 0.015f, -0.344f);
                    }
                }
            }
        }*/
    }

    public void turnOffLights()
    {
        transform.localPosition = new Vector3(-0.9f, Random.Range(-0.260f,0.125f), transform.localPosition.z);
        indicatorLight.color = Color.red;
    }
}