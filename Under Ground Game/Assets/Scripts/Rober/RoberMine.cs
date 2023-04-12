using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoberMine : MonoBehaviour
{
    public bool mine, mining, inCollision;
    public Tilemap currentTilemap;
    RoberMovement movement;
    RoberInventory inventory;
    public float miningSpeed;

    void Start()
    {
        movement = FindObjectOfType<RoberMovement>();
        inventory = FindObjectOfType<RoberInventory>();
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
        yield return new WaitForSeconds(miningSpeed);
        var tilePos = currentTilemap.WorldToCell(transform.position);
        if(currentTilemap.GetTile(tilePos) != null)
        {
            if(inventory.currentWeight + currentTilemap.gameObject.GetComponent<Item>().weight <= inventory.maxWeight)
            {
                currentTilemap.SetTile(tilePos, null);
                inventory.AddItem(currentTilemap.gameObject.GetComponent<Item>());
                FindObjectOfType<RoberManager>().drillCurrentHealth--;
            }
            else
            {
                movement.GoToBase();
            }
            
        }
        
        mining = false;
        movement.GoTo(movement.currentDestination);
    }
}
