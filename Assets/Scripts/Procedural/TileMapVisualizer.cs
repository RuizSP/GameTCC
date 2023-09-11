using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap, wallTileMap, platformTileMap, decorationsTileMap;

    [SerializeField] private TileBase floorTile, wallTop;

    [SerializeField] private List<TileBase> decorationsTile = new List<TileBase>(); 

    [SerializeField] private GameObject Player;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
       PaintTiles(floorPositions, floorTileMap, floorTile); 

    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        } 
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
       var tilePosition = tilemap.WorldToCell((Vector3Int)position); 
       tilemap.SetTile(tilePosition, tile);

    }

    public void PaintSingleBasicWall(Vector2Int position){
        PaintSingleTile(wallTileMap, wallTop, position);
          wallTileMap.GetComponent<Renderer>().sortingOrder = 3;
    }

    public void PaintDecorations(List<Vector2Int> position){
        foreach (var item in position)
        {
            PaintSingleTile(decorationsTileMap, decorationsTile[Random.Range(0, decorationsTile.Count)], item);    
        }
        decorationsTileMap.GetComponent<Renderer>().sortingOrder = 2;
        
    }

    //public void PlayerSpawner(Vector3 position){
        //Player.transform.position = position;
    //}


    /*public void PaintEnemies(List<Vector3Int> positions, List<GameObject> Enemies){
        foreach (var position in positions)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Count-1)], position, Quaternion.identity);
        }
    }*/

    public void Clear()
    {
        decorationsTileMap.ClearAllTiles();
        platformTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        floorTileMap.ClearAllTiles();

    }

}
