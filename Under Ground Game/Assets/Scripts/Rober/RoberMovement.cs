using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberMovement : MonoBehaviour
{
    public float baseSpeed;

    public bool alreadyGoing;
    Vector3 currentDestination;
    void Start()
    {
        alreadyGoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyGoing && Vector3.Distance(transform.position, currentDestination) > 0.5f)
        {
            transform.LookAt(currentDestination);
            transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
        }
        else
        {
            alreadyGoing = false;
        }
    }

    public void GoTo(Vector3 destination)
    {
        currentDestination = destination;
        alreadyGoing = true;
    }
}
