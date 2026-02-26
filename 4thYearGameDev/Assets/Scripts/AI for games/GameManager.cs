using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
       [Header("Pathfinding")]
       public TileBase[] enemyUnwalkableCollisionTilesArray;
       public TileBase preferredEnemyPathTile;
}
