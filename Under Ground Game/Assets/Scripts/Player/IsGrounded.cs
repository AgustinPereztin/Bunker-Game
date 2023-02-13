using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    PlayerMovement pm;
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Debug.Log("Grounded");
            pm.isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Debug.Log("!Grounded");
            pm.isGrounded = false;
        }
    }
}
