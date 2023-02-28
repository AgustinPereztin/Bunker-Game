using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorFunctionality : MonoBehaviour
{
    public BoxCollider collider;
    public GameObject door;

    public Animator animator;
    public Text openDoorText;
    public Text closeDoorText;


    public bool isDoorOpen;
    public bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        openDoorText.gameObject.SetActive(false);
        closeDoorText.gameObject.SetActive(false);
        inRange = false;
        isDoorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (isDoorOpen)
            {
                openDoorText.gameObject.SetActive(false);
                closeDoorText.gameObject.SetActive(true);
            }
            else
            {
                openDoorText.gameObject.SetActive(true);
                closeDoorText.gameObject.SetActive(false);
            }
        }
        else
        {
            openDoorText.gameObject.SetActive(false);
            closeDoorText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        inRange = true;

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Interact");
            StartCoroutine("ChangeCollider");
            if (isDoorOpen)
            {
                isDoorOpen = false;
            }
            else
            {
                isDoorOpen = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    IEnumerator ChangeCollider()
    {
        //yield return new WaitForSeconds(1f);
        door.GetComponent<MeshCollider>().enabled = false;
        
        yield return new WaitForSeconds(1f);
        door.GetComponent<MeshCollider>().enabled = true;
    }
}
