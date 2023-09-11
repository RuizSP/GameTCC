using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;


public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f,1)] private float roomPercent =0.8f;

    
    [SerializeField] private int platformHeight = 3;
    [SerializeField] private Tilemap platformTilemap;
    [SerializeField] private TileBase platformTile;
    [SerializeField] private int decorationQuantity = 10;
    
    
    //[SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    //[SerializeField] private int enemyQuantity = 10;

    protected override void RunProceduralGeneration()
    {
     CorridorFirstGeneration(); 

    }

    public void PositionSetter(Vector2Int pos){
         startPosition = pos;
         GenerateDungeon();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        List <List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        
        platformTilemap.GetComponent<Renderer>().sortingOrder = 2;

             
        List <Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        floorPositions.UnionWith(roomPositions);
        
        for (var i = 0; i<corridors.Count; i++)
        {
            //corridors[i] = IncreaseCorridorsizeByOne(corridors[i]);
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);

            
        }

        List<Vector2Int> decorationsPosition = CreateDecorations(floorPositions);
        tilemapVisualizer.PaintDecorations(decorationsPosition);
        //tilemapVisualizer.PlayerSpawner(new Vector3(startPosition.x,startPosition.y, 0));  
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        //CreatePlatforms(floorPositions, corridors); 

       

    }
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
      var currentPosition = startPosition;
      potentialRoomPositions.Add(currentPosition);
      List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
      for (int i = 0; i<corridorCount;i++)
      {
         var path =  ProceduralGenerationAlgorithms.RandonWalkCorridor(currentPosition, corridorLength);
         corridors.Add(path);
         currentPosition = path[path.Count-1];
         potentialRoomPositions.Add(currentPosition);
         floorPositions.UnionWith(path);
      }
       return corridors; 
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
       HashSet<Vector2Int>roomPositions = new HashSet<Vector2Int>();
       int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
       List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => System.Guid.NewGuid()).Take(roomToCreateCount).ToList();
       foreach (var roomPosition in roomsToCreate)
       {
        var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
        roomPositions.UnionWith(roomFloor);
       }
       return roomPositions;
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if(floorPositions.Contains(position + direction))
                {
                    neighboursCount ++;

                }
                
            }
            if (neighboursCount ==1)
            {
               deadEnds.Add(position); 
                
            } 
        }
        return deadEnds;

    }
    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var position in deadEnds)
        {
            if(roomPositions.Contains(position) == false)
            {
                var roomFloor = RunRandomWalk(randomWalkParameters, position);
                roomPositions.UnionWith(roomFloor);
            } 
        }

    }

    public List<Vector2Int> IncreaseCorridorsizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;
        for (var i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i-1];
            if(previewDirection != Vector2Int.zero && directionFromCell != previewDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y =-1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                    }
                }
                previewDirection = directionFromCell;
            }else
            {
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i-1]);
                newCorridor.Add(corridor[i-1]+ newCorridorTileOffset);

            }
        }
        return newCorridor;
    }
    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            return Vector2Int.right; 
        }
        if (direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }
        if (direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        if (direction == Vector2Int.left)
        {
            return Vector2Int.up; 
        }
        
        return Vector2Int.zero;
    }
    public List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor){
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                    
                }
                
            }
            
        }
        return newCorridor;


    }




    private void CreatePlatforms(HashSet<Vector2Int> floorPositions, List<List<Vector2Int>> corridors)
{
    foreach (var position in floorPositions)
    {
        // verifica se a posição atual é um corredor
        bool isCorridor = false;
        foreach (var corridor in corridors)
        {
            if (corridor.Contains(position))
            {
                isCorridor = true;
                break;
            }
        }

        // verifica se a posição atual está em uma sala (não é um corredor) e se está em uma altura que possa ter uma plataforma
        if (!isCorridor && position.y % platformHeight == 0)
        {
            // gera um número aleatório entre 0 e 1 para determinar se será criada uma plataforma nesta posição
            float randomValue = Random.Range(0f, 1f);
            if (randomValue < 0.5f)
            {
                // cria uma plataforma de tamanho aleatório ao redor da posição atual
                int platformWidth = Random.Range(1, 4);
                for (int x = -platformWidth; x <= platformWidth; x++)
                {
                    Vector2Int platformPosition = new Vector2Int(position.x + x, position.y);
                    if (!floorPositions.Contains(platformPosition))
                    {
                        platformTilemap.SetTile(new Vector3Int(platformPosition.x, platformPosition.y, 0), platformTile);
                    }
                }
            }
        }
    }
}
    /*public  List<Vector3Int> EnemyGenerator(HashSet<Vector2Int> positions){

            List<Vector3Int> possibleEnemyPosition = new List<Vector3Int>();
            List<Vector3Int> enemyPosition = new List<Vector3Int>();
            foreach (var position in positions)
            {
                possibleEnemyPosition.Add(new Vector3Int(position.x, position.y, 0));

            }
            for (int i =0; i< possibleEnemyPosition.Count -1; i++){
                if(enemyPosition.Contains(possibleEnemyPosition[i]) || possibleEnemyPosition[i] == new Vector3Int(startPosition.x, startPosition.y, 0)){
                    continue;
                }else if (enemyPosition.Count >= enemyQuantity){
                    break;
                }else{
                    enemyPosition.Add(possibleEnemyPosition[Random.Range(0, possibleEnemyPosition.Count-1)]);
                }    
                    
                
            }

            return enemyPosition;

    }*/
   /* public  List<Vector2Int> CreateDecorations(HashSet<Vector2Int> positions){

            List<Vector2Int> possibleDecorationPositions = new List<Vector2Int>();
            List<Vector2Int> decoratonPositions = new List<Vector2Int>();
            foreach (var position in positions)
            {
                possibleDecorationPositions.Add(new Vector2Int(position.x, position.y));

            }
            for (int i =0; i< possibleDecorationPositions.Count -1; i++){
                if(decoratonPositions.Contains(possibleDecorationPositions[i])){
                    continue;
                }else if (decoratonPositions.Count >= decorationQuantity){
                    break;
                }else{
                    decoratonPositions.Add(possibleDecorationPositions[Random.Range(0, possibleDecorationPositions.Count-1)]);
                }    
                    
                
            }

            return decoratonPositions;

    }*/

    private List<Vector2Int> CreateDecorations(HashSet<Vector2Int> floorPositions)
{
    List<Vector2Int> decorationsPosition = new List<Vector2Int>();
    int decorationCount = 0;
    float minDistance = 5f;
    while (decorationCount < decorationQuantity)
    {
        bool validPosition = false;
        Vector2Int randomFloorPosition = new Vector2Int(0,0);
        while (!validPosition)
        {
            randomFloorPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            validPosition = true;
            foreach (Vector2Int decorationPosition in decorationsPosition)
            {
                if (Vector2Int.Distance(randomFloorPosition, decorationPosition) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }
        }
        decorationsPosition.Add(randomFloorPosition);
        decorationCount++;
    }
    return decorationsPosition;
}
}
