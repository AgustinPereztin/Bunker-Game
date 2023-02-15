using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{
    public Transform standingPosition, leavingPosition;
    Quaternion cameraRotation;
    PlayerMovement pm;
    GameObject playerCam;
    public GameObject interectText;
    public bool inRange, open, onCooldown;

    public float cooldown, pSpeed, rSpeed;
    void Start()
    {
        interectText.SetActive(false);
        pm = FindObjectOfType<PlayerMovement>();
        playerCam = GameObject.Find("Player Cam");
        cameraRotation = playerCam.transform.localRotation;
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !open && !onCooldown)
        {
            open = true;
            interectText.SetActive(false);
            pm.canMove = false;
            pm.InteractComputer();
            onCooldown = true;
        }

        if (open && !onCooldown && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            onCooldown = true;
            open = false;
            pm.InteractComputer();
        }

        if(open && onCooldown)
        {
            var pstep = pSpeed * Time.deltaTime;
            var rstep = rSpeed * Time.deltaTime;
            pm.transform.position = Vector3.MoveTowards(pm.transform.position, standingPosition.localPosition, pstep);

            pm.transform.rotation = Quaternion.Lerp(pm.transform.rotation, standingPosition.transform.rotation, rstep);

            playerCam.transform.localRotation = Quaternion.Slerp(playerCam.transform.localRotation, cameraRotation, rstep);
            pm.rotationX = 0;

            if (Vector3.Distance(pm.transform.position, standingPosition.localPosition) < 0.001f 
                && pm.transform.rotation == standingPosition.rotation)
            {
                onCooldown = false;
            }
        }
        else if(!open && onCooldown)
        {
            var pstep = pSpeed * Time.deltaTime;
            var rstep = rSpeed * Time.deltaTime;
            pm.transform.position = Vector3.MoveTowards(pm.transform.position, leavingPosition.localPosition, pstep);

            pm.transform.rotation = Quaternion.Lerp(pm.transform.rotation, leavingPosition.rotation, rstep);

            if (Vector3.Distance(pm.transform.position, leavingPosition.localPosition) < 0.001f && pm.transform.rotation == leavingPosition.rotation)
            {
                onCooldown = false;
                pm.canMove = true;
                interectText.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            inRange = true;
            interectText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            inRange = false;
            interectText.SetActive(false);
        }
    }
}
