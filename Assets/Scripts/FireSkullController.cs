using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkullController : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player exists and is active
        if (player != null && player.gameObject.activeSelf)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.velocity = directionToPlayer * speed;

            // Rotate the FireSkull towards the player
            transform.right = - directionToPlayer;
        }
    }

    // OnCollisionEnter2D is called when the FireSkull collides with another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the FireSkull
            Destroy(this.gameObject);
        }
    }
}
