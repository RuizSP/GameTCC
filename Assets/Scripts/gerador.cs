using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gerador : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int steps = 10;
    [SerializeField] private int stepsHeight = 20;
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private int bossRoomCount = 1;

    CardinalDirections directions = new CardinalDirections();
    private HashSet<Vector2Int> posicoes = new HashSet<Vector2Int>(); 
    void Start(){
        generateLevel();
    }
   void generateLevel(){
    var startPos = Vector2Int.zero;
    var currentPos = startPos;
    posicoes.Add(startPos);
    
    Vector2Int previousDirection = Vector2Int.zero; // Armazenar a direção anterior
    
    for(int i = 0; i < steps; i++){
        Vector2Int direction;
        do {
            direction = directions.getCardinalDirections();
        } while (isOppositeDirection(previousDirection, direction)); // Garantir que não seja a direção oposta
        
        if(direction == directions.esquerda) {
            currentPos.x -= stepsHeight;
        }
        else if(direction == directions.direita) {
            currentPos.x += stepsHeight;
        }
        else if(direction == directions.up) {
            currentPos.y += stepsHeight;
        }
        else if(direction == directions.down) {
            currentPos.y -= stepsHeight;
        }
        
        posicoes.Add(currentPos);
        previousDirection = direction; // Atualizar a direção anterior
    }
    SetBossRoom();
    foreach (var item in posicoes){
        Instantiate(room, new Vector3(item.x, item.y, 0f), Quaternion.identity); 

    }

    void SetBossRoom()
    {
        List<Vector2Int> lista = new  List<Vector2Int>();
        foreach (var item in posicoes)
        {
            lista.Add(item);
        }
        for (int i =0; i< bossRoomCount; i ++)
        {
            Vector2Int bossroomPosition  = lista[Random.Range((posicoes.Count/2), posicoes.Count)];
            Instantiate(bossRoom, new Vector3(bossroomPosition.x, bossroomPosition.y, 0f), Quaternion.identity); 
            posicoes.Remove(bossroomPosition);
        }

    }

}

bool isOppositeDirection(Vector2Int dir1, Vector2Int dir2){
    return dir1 == -dir2;
}

}

public class CardinalDirections
{
    public Vector2Int esquerda = Vector2Int.left;
    public Vector2Int direita =Vector2Int.right;
    public Vector2Int up = Vector2Int.up;
    public Vector2Int down = Vector2Int.down;
    
    public Vector2Int getCardinalDirections(){
        int direction = Random.Range(0, 6);
        switch(direction){
            case 0:
                return esquerda;
            case 1:
                return direita;
            case 2:
                return esquerda;
            case 3:
                return direita;
            case 4:
                return up;
            case 5:
                return down;
            
        }
        return direita;
    }
}
