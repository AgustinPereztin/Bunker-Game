using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public Animator anim;
    public Text doorText;
    public bool isDoorOpen;

    private void Start()
    {
        isDoorOpen = false;
        doorText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //doorText.gameObject.SetActive(true);
        if (!isDoorOpen)
        {
            anim.SetTrigger("Open");
            isDoorOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //doorText.gameObject.SetActive(false);
        if (isDoorOpen)
        {
            anim.SetTrigger("Close");
            isDoorOpen = false;
        }
    }
}
