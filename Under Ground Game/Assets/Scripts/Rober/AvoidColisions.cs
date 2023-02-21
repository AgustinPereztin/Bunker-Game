using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AvoidColisions : MonoBehaviour
{
    public Vector3 target;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    public Path path;
    public int currentWaypoint = 0;
    bool reachEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;

    RoberMovement movement;
    void Start()
    {
        movement = GetComponent<RoberMovement>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(ModelRotation());
        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target, OnPathComplete);
        }
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            movement.alreadyGoing = false;
            StopAllCoroutines();
            return;
        }
        else
        {
            reachEndOfPath = false;
        }


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance && currentWaypoint < path.vectorPath.Count)
        {
            currentWaypoint++;
        }
    }

    public void StartCoroutineLoca()
    {
        StartCoroutine(ModelRotation());
    }
    IEnumerator ModelRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (currentWaypoint < path.vectorPath.Count)
            {
                var dir = path.vectorPath[currentWaypoint] - movement.model.transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle -= 90;
                movement.model.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
