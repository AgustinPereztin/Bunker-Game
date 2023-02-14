using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObject : MonoBehaviour
{
    public BoxCollider collider;
    public UIControllerInGame uIController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            uIController.ShowEInteraction();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uIController.HideEInteraction();
        }
    }
}
