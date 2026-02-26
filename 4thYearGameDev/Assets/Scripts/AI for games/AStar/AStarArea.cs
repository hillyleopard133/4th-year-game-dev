using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarArea : MonoBehaviour
{
    public Vector2Int gridLowerBounds;
    public Vector2Int gridUpperBounds;
    
    public Grid grid;
    public Tilemap collisionTilemap;
    public Tilemap frontTilemap;
    
    public TileBase collisionTileToPlace;
    public int numberOfTilesToPlace = 20;
    public TileBase obstacleCollisionTile;
    
    [HideInInspector] public int[,] aStarMovementPenalty;

    private void Start()
    {
        AddObstaclesAndPreferredPaths();
    }

    public void PlaceRandomCollisionTiles()
    {
        for (int i = 0; i < numberOfTilesToPlace; i++)
        {
            int x = Random.Range(gridLowerBounds.x, gridUpperBounds.x + 1);
            int y = Random.Range(gridLowerBounds.y, gridUpperBounds.y + 1);

            Vector3Int pos = new Vector3Int(x, y, 0);

            collisionTilemap.SetTile(pos, collisionTileToPlace);
        }

        AddObstaclesAndPreferredPaths();
    }
    
    public void AddObstaclesAndPreferredPaths()
    {
        aStarMovementPenalty = new int[gridUpperBounds.x - gridLowerBounds.x + 1, gridUpperBounds.y - gridLowerBounds.y + 1];

        for (int x = 0; x < gridUpperBounds.x - gridLowerBounds.x + 1; x++)
        {
            for (int y = 0; y < gridUpperBounds.y - gridLowerBounds.y + 1; y++)
            {
                aStarMovementPenalty[x, y] = Settings.defaultAStarMovementPenalty;
                TileBase tile = collisionTilemap.GetTile(new Vector3Int(x + gridLowerBounds.x, y + gridLowerBounds.y, 0));

                if(tile == null) continue;
                foreach (TileBase collisionTile in GameManager.Instance.enemyUnwalkableCollisionTilesArray)
                {
                    if (collisionTile == tile)
                    {
                        aStarMovementPenalty[x, y] = 0;
                        break;
                    }
                }
                if (tile == GameManager.Instance.preferredEnemyPathTile)
                {
                    aStarMovementPenalty[x, y] = Settings.preferredPathAStarMovementPenalty;
                }
            }
        }
    }
}