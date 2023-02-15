using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPc : MonoBehaviour
{
    public CharacterController characterController;
    public BasicInteraction thePc;
    public Camera minimapCamera;

    public float minMap, maxMap;    

    public float speed, scrollSpeed;
    Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        if (thePc.open && !thePc.onCooldown)
        {
            //Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            moveDirection = (forward * (Input.GetAxis("Vertical") * speed) + (right * (Input.GetAxis("Horizontal") * speed)));

            characterController.Move(moveDirection * Time.deltaTime);

            //Zoom

            if (Input.GetAxis("Mouse ScrollWheel") > 0f && minimapCamera.orthographicSize < maxMap)
            {
                minimapCamera.orthographicSize += scrollSpeed * Time.deltaTime;
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0f && minimapCamera.orthographicSize > minMap)
            {
                minimapCamera.orthographicSize -= scrollSpeed * Time.deltaTime;
            }
        }
    }
}
