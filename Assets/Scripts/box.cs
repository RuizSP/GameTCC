using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float initialSpeed = 10f;
    private float launchAngle = 30f; 
    public int direction;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       float radians= launchAngle * Mathf.Deg2Rad;
       float horizontalSpeed = initialSpeed * Mathf.Cos(radians);
       float verticalSpeed = initialSpeed * Mathf.Sin(radians);
       rb.velocity = new Vector3(direction*horizontalSpeed, verticalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.layer == 3){
            Destroy(this.gameObject, 0.1f);
        }

    }
}
