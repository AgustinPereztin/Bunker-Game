using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class RoberMovement : MonoBehaviour
{
    public bool miningMode;
    public RoberMine[] mine;
    AvoidColisions navMesh2d;
    public float baseSpeed;
    public GameObject model;

    public bool alreadyGoing;
    public Vector3 currentDestination;
    void Start()
    {
        alreadyGoing = false;
        mine = FindObjectsOfType<RoberMine>();
        navMesh2d = GetComponent<AvoidColisions>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mine.Length; i++)
        {
            mine[i].mine = miningMode;
        }

        model.transform.position = transform.position;

        if (alreadyGoing && Vector3.Distance(transform.position, currentDestination) > 0.5f && miningMode)
        {
            transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
        }
        else if(alreadyGoing && Vector3.Distance(transform.position, currentDestination) <= 0.5f)
        {
            alreadyGoing = false;
        }

        if (miningMode)
        {
            navMesh2d.enabled = false;
        }
    }

    public void GoTo(Vector3 destination)
    {
        if (miningMode)
        {
            currentDestination = destination;
            alreadyGoing = true;
            transform.LookAt(currentDestination);
            var dir = destination - model.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            model.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        

        if (!miningMode)
        {
            navMesh2d.enabled = true;
            navMesh2d.target = destination;
            navMesh2d.StartCoroutineLoca();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for(int i = 0; i < mine.Length; i++)
        {
            mine[i].inCollision = true;
            mine[i].currentTilemap = collision.gameObject.GetComponent<Tilemap>();
        }
        
        alreadyGoing = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for (int i = 0; i < mine.Length; i++)
        {
            mine[i].inCollision = false;
        }
        alreadyGoing = true;
    }
}
