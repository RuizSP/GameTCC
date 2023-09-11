using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public GameObject baseCamera;
    private float length;
    private float startPos;
    public float ParallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; 
;
    }

    // Update is called once per frame
    void Update()
    {
        float temp =  baseCamera.transform.position.x * (1 - ParallaxEffect);
        float Distance = baseCamera.transform.position.x * ParallaxEffect;
        transform.position = new Vector3(startPos + Distance, transform.position.y, transform.position.z );
        if ( startPos + length/2 < temp ) {

            startPos += length; 
        
        }else if(temp < startPos- length/2)
        {
            startPos -= length;
        }

        
    }
}
