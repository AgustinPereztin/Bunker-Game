using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public Animator anim;
    public Text doorText;

    private void Start()
    {
        doorText.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        doorText.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Interact");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        doorText.gameObject.SetActive(false);
    }
}
