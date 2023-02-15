using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;

    public Camera playerCam;

    public float walkingSpeed;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookLimit = 45f;
    public float rotationX = 0;


    public bool canMove = true;
    public bool isGrounded = true;
    Vector3 moveDirection = Vector3.zero;

    CharacterController characterController;

    void Start()
    {
        animator.enabled = false;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    void Update()
    {
        if (canMove)
        {
            //Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            moveDirection = (forward * (Input.GetAxis("Vertical") * walkingSpeed) + (right * (Input.GetAxis("Horizontal") * walkingSpeed)));

            if (!isGrounded)
            {
                moveDirection.y -= gravity * 1000 * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);


            //Camera Movement

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);

            playerCam.transform.localRotation = Quaternion.Euler(rotationX,0,0);
            transform.rotation *= Quaternion.Euler(0,Input.GetAxis("Mouse X") * lookSpeed,0);
        }
    }

    public void InteractComputer()
    {
        animator.enabled = !animator.enabled;   
        animator.SetTrigger("InteractPc");
    }
}
