using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool hasObstacle = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy is at the edge of the platform or has an obstacle
        bool atEdge = !IsGrounded();
        bool obstacleDetected = CheckObstacle();

        // Change direction if at the edge or an obstacle is detected
        if (atEdge || obstacleDetected)
        {
            Flip();
        }

        // Move the enemy in the current direction
        float moveDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    // Check if the enemy is grounded
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Check for obstacles using Raycast
    bool CheckObstacle()
    {
        Vector2 raycastOrigin = isFacingRight ? groundCheck.position : groundCheck.position - Vector3.right * groundCheckRadius * 2f;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckRadius * 2f, groundLayer);
        return hit.collider == null;
    }

    // Flip the enemy's sprite and direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Set hasObstacle to true only when changing direction
        hasObstacle = true;
    }

    // Draw the detection area in the Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
