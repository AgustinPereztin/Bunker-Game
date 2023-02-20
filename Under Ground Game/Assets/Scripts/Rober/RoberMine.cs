using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoberMine : MonoBehaviour
{
    public bool mine, mining, inCollision;
    public Tilemap currentTilemap;
    RoberMovement movement;
    void Start()
    {
        movement = FindObjectOfType<RoberMovement>();
    }

    void Update()
    {
        if(mine && inCollision && !mining)
        {
            mining = true;
            StartCoroutine(Mine());
        }
    }

    IEnumerator Mine()
    {
        yield return new WaitForSeconds(1f);
        var tilePos = currentTilemap.WorldToCell(transform.position);
        currentTilemap.SetTile(tilePos, null);
        mining = false;
        movement.alreadyGoing = true;
    }
}
