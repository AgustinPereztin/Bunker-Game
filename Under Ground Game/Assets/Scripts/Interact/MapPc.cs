using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPc : MonoBehaviour
{
    public CharacterController characterController;
    public BasicInteraction thePc;
    public Camera minimapCamera;
    public GameObject crossHair, goToPointPrefab;

    GameObject currentMark;
    public float minMap, maxMap;    

    public float speed, scrollSpeed;
    Vector3 moveDirection = Vector3.zero;


    private void Start()
    {
        crossHair.SetActive(false);
    }
    void Update()
    {
        if (thePc.open && !thePc.onCooldown)
        {
            crossHair.SetActive(true);

            //Movement
            Vector3 forward = transform.TransformDirection(Vector3.up);
            Vector3 right = transform.TransformDirection(Vector3.right);

            moveDirection = (forward * (Input.GetAxis("Vertical") * speed) + (right * (Input.GetAxis("Horizontal") * speed)));

            characterController.Move(moveDirection * Time.deltaTime);

            //Zoom

            if (Input.GetAxis("Mouse ScrollWheel") > 0f && minimapCamera.orthographicSize < maxMap)
            {
                minimapCamera.orthographicSize -= scrollSpeed * Time.deltaTime;
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0f && minimapCamera.orthographicSize > minMap)
            {
                minimapCamera.orthographicSize += scrollSpeed * Time.deltaTime;
            }

            //Mark

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Quaternion rotation = minimapCamera.transform.rotation;
                Vector3 position = new Vector3(minimapCamera.transform.position.x, minimapCamera.transform.position.y, -15.51f);
                if(currentMark != null)
                {
                    Destroy(currentMark);
                }

                currentMark = Instantiate(goToPointPrefab, position, rotation);

                FindObjectOfType<RoberMovement>().GoTo(currentMark.transform.position);
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }
}
