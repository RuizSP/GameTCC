using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] EnemiesList;
    void Start()
    {
        ChooeARandomEnemy();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooeARandomEnemy(){
        var enemyChoosen = EnemiesList[Random.Range(0, EnemiesList.Length)];
        Instantiate(enemyChoosen, this.transform.position, Quaternion.identity);
    }
}
