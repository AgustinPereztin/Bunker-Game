using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionActivate : MonoBehaviour
{
    public GameObject fpsCamera;
    public GameObject sleepingText;
    public LayerMask bed;
    public float range;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rayHit;
        if(Physics.Raycast(fpsCamera.transform.position,fpsCamera.transform.forward, out rayHit, range, bed))
        {
            sleepingText.SetActive(true);
            rayHit.collider.GetComponent<Bed>().inRange = true;
        }
        else
        {
            sleepingText.SetActive(false);
            FindObjectOfType<Bed>().inRange = false;
        }
    }
}
