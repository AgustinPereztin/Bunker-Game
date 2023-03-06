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
    private CircleCollider2D roberCollider;
    public bool alreadyGoing;
    public Vector3 currentDestination;
    public Transform basePosition;
    bool alreadyGoingToBase;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public BasicInteraction thePc;
    void Start()
    {
        alreadyGoing = false;
        mine = FindObjectsOfType<RoberMine>();
        roberCollider = GetComponent<CircleCollider2D>();
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

        if(alreadyGoingToBase && Vector3.Distance(transform.position, currentDestination) <= 0.5f)
        {
            alreadyGoingToBase = false;
        }

        if (alreadyGoing && Vector3.Distance(transform.position, currentDestination) > 0.5f && !miningMode)
        {
            HandleMovement();
        }

        if (Input.GetKeyDown(KeyCode.P) && thePc.open)
        {
            FindObjectOfType<Testing>().CalculatePath();
            miningMode = !miningMode;

            if (!miningMode)
            {
                roberCollider.isTrigger = true;
                transform.position = new Vector3(((int)transform.position.x), ((int)transform.position.y), transform.position.z);
            }
            else
            {
                roberCollider.isTrigger = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.O) && thePc.open)
        {
            GoToBase();
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

    public void GoToBase()
    {
        if (!alreadyGoingToBase)
        {
            transform.position = new Vector3(((int)transform.position.x), ((int)transform.position.y), transform.position.z);
            FindObjectOfType<Testing>().CalculatePath();
            GoTo(basePosition.position);
            alreadyGoingToBase = true;
            miningMode = false;
            roberCollider.isTrigger = true;
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
