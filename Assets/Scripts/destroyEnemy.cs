using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int vida = 100;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(vida<= 0){
            
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.layer == 8){
            Destroy(this.gameObject);
        }
    }
}
