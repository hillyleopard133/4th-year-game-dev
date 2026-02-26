using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
       [SerializeField] public Player player;
       
       [Header("Pathfinding")]
       public TileBase[] enemyUnwalkableCollisionTilesArray;
       public TileBase preferredEnemyPathTile;
}
