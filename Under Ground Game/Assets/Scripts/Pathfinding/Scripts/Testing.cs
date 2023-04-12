using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using UnityEngine.Tilemaps;
public class Testing : MonoBehaviour {
    
    [SerializeField] private RoberMovement characterPathfinding;
    public Pathfinding pathfinding;
    public Tilemap collisionableTiles;

    private void Start() 
    {
        pathfinding = new Pathfinding(400, 300);
        CalculatePath();
    }

    public void CalculatePath()
    {
        for (int x = 0; x < pathfinding.grid.GetWidth(); x++)
        {
            for (int y = 0; y < pathfinding.grid.GetHeight(); y++)
            {
                var tilePos = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y));

                if (collisionableTiles.GetTile(tilePos) != null)
                {
                    pathfinding.GetNode(x, y).SetIsWalkable(false);
                }
                else
                {
                    pathfinding.GetNode(x, y).SetIsWalkable(true);
                }
            }
        }
    }

    public bool IsWalkable(Vector3 destination)
    {
        if (pathfinding.GetNode((int)destination.x, (int)destination.y).isWalkable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator CalculateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CalculatePath();
        }
    }
}
