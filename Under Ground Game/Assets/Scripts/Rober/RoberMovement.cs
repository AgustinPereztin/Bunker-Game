using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoberMovement : MonoBehaviour
{
    public bool miningMode;
    public RoberMine[] mine;
    public float baseSpeed;
    public GameObject model;

    public bool alreadyGoing;
    public Vector3 currentDestination;


    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    void Start()
    {
        alreadyGoing = false;
        mine = FindObjectsOfType<RoberMine>();
    }

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

        if (alreadyGoing && Vector3.Distance(transform.position, currentDestination) > 0.5f && !miningMode)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * baseSpeed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
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
            alreadyGoing = true;

            currentPathIndex = 0;
            pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), destination);

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }
        }
    }

    private void StopMoving()
    {
        alreadyGoing = false;
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
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
