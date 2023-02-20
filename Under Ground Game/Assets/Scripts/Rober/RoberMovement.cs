using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberMovement : MonoBehaviour
{
    public RoberMine[] mine;
    public float baseSpeed;
    public GameObject model;

    public bool alreadyGoing;
    Vector3 currentDestination;
    void Start()
    {
        alreadyGoing = false;
        mine = FindObjectsOfType<RoberMine>();
    }

    // Update is called once per frame
    void Update()
    {

        if (alreadyGoing && Vector3.Distance(transform.position, currentDestination) > 0.5f)
        {
            transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
            model.transform.position = transform.position;
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
        transform.LookAt(currentDestination);
        var dir = destination - model.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        model.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject + " collision");
        for(int i = 0; i < mine.Length; i++)
        {
            mine[i].inCollision = true;
        }
        
        alreadyGoing = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject + " collision");
        for (int i = 0; i < mine.Length; i++)
        {
            mine[i].inCollision = false;
        }
        alreadyGoing = true;
    }
}
