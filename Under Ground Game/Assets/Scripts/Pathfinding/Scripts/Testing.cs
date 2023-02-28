using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using UnityEngine.Tilemaps;
public class Testing : MonoBehaviour {
    
    //[SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    [SerializeField] private RoberMovement characterPathfinding;
    private Pathfinding pathfinding;
    public Tilemap collisionableTiles;

    private void Start() 
    {
        pathfinding = new Pathfinding(200, 150);

        for(int x = 0; x < pathfinding.grid.GetWidth(); x++)
        {
            for(int y = 0; y < pathfinding.grid.GetHeight(); y++)
            {
                var tilePos = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x,y));
                
                if (collisionableTiles.GetTile(tilePos) != null)
                {
                    pathfinding.GetNode(x, y).SetIsWalkable(false);
                }

                var tilePos2 = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y) + new Vector3(1, -1, 0));

                if (collisionableTiles.GetTile(tilePos2) != null)
                {
                    pathfinding.GetNode(x + 1, y - 1).SetIsWalkable(false);
                }

                var tilePos3 = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y) + new Vector3(1, 0, 0));

                if (collisionableTiles.GetTile(tilePos3) != null)
                {
                    pathfinding.GetNode(x + 1, y).SetIsWalkable(false);
                }

                var tilePos4 = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y) + new Vector3(0, -1, 0));

                if (collisionableTiles.GetTile(tilePos4) != null)
                {
                    pathfinding.GetNode(x, y - 1).SetIsWalkable(false);
                }

                var tilePos5 = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y) + new Vector3(-1, -1, 0));

                if (collisionableTiles.GetTile(tilePos5) != null)
                {
                    pathfinding.GetNode(x - 1, y - 1).SetIsWalkable(false);
                }

                var tilePos6 = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y) + new Vector3(-1, 0, 0));

                if (collisionableTiles.GetTile(tilePos6) != null)
                {
                    pathfinding.GetNode(x - 1, y).SetIsWalkable(false);
                }

                var tilePos7 = collisionableTiles.WorldToCell(pathfinding.grid.GetWorldPosition(x, y) + new Vector3(0, 1, 0));

                if (collisionableTiles.GetTile(tilePos7) != null)
                {
                    pathfinding.GetNode(x, y + 1).SetIsWalkable(false);
                }
            }
        }
    }

    private void Update() {
        /*if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 2f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 2f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            characterPathfinding.GoTo(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }*/
    }

}
