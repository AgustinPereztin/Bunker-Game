using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanelMinigame1 : MonoBehaviour
{
    public GameObject lever1, lever2, lever3, lever4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Luces fuera");
            turnOffLights();
        }
        //Cursor.visible = false;
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
        }
    }

    public void turnOffLights()
    {
        lever1.transform.localPosition = new Vector3(-0.9f, Random.Range(-0.260f,0.125f), 0.344f);
        lever2.transform.localPosition = new Vector3(-0.9f, Random.Range(-0.260f, 0.125f), 0.141f);
        lever3.transform.localPosition = new Vector3(-0.9f, Random.Range(-0.260f, 0.125f), -0.141f);
        lever4.transform.localPosition = new Vector3(-0.9f, Random.Range(-0.260f, 0.125f), -0.344f);
    }
}