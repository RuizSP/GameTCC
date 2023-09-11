using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBase : MonoBehaviour
   
{
    public float maxDistance;
    public float pontoInicio;
    public float direction;
    public bool canVoltar = false;

    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = transform.position.x + 2;
        pontoInicio = transform.position.x;
        rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= maxDistance)
        {
            direction = -1;
          
        }

        else {
            if(transform.position.x < maxDistance)
            {
                direction = 1;
                
            }
        }
        rig.velocity += new Vector2(direction * Time.deltaTime, 0f);
       if (rig.velocity.x >=0)
        {
            transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
 
        }
           
		


    }
}
