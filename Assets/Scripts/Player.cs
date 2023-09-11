using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] wallCheckers = new Transform[2];
    [SerializeField] private float wallCheckersRadius = 0.2f;



    public float speed = 3f;
    public float jumpForce = 5f;
    public int maxJumpCount = 2;
    public int life;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private AudioSource audioBase;
    public AudioClip audioDeath;
    public AudioClip audioDamage;
    public AudioClip audioAttack;
    public AudioClip audioJump;
    public AudioClip audioWalk;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    private Rigidbody2D rig;
    private Animator animator;
    [SerializeField] private Slider lifeBar;
    private int jumpCount;


    private bool isJumping;
    private bool isOnWall;
    [SerializeField] private float wallJumpTime = 0.2f;
    [SerializeField] private float wallJumpForce = 8f;
    private float wallJumpDirection;

    void Start()
    {
        life = valueManagement.life;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioBase = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (life > 0)
        {
            Walk();
            Jump();
            Attack();
            WallSlide();
            WallJump();
            valueManagement.life = life;
            if (!IsGrounded() && !IsOnWall())
            {
                animator.SetBool("jump", true);
            }
            
        }
        else
        {
            Death();
        }
        if (isJumping && IsOnWall())
        {
            isJumping = false;
            isOnWall = false;
            rig.velocity = new Vector2(-wallJumpDirection * wallJumpForce, jumpForce);
            StartCoroutine(DisableMovement(wallJumpTime));
        }
    }

    void Walk()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0f)
        {
            if(IsGrounded()){
                animator.SetBool("walk", true);
            }
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement < 0f)
        {
            if(IsGrounded()){
                animator.SetBool("walk", true);
            }
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

     void WallSlide()
    {
        var left = Input.GetKey(KeyCode.A);
        var right = Input.GetKey(KeyCode.D);

        if (left || right)
        {
            if (!IsGrounded() && IsOnWall())
            {
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y / 4);
            }
        }
    }

    void WallJump()
    {
        if (IsOnWall() && Input.GetButtonDown("Jump") && !IsGrounded())
        {
            isJumping = true;
            animator.SetBool("jump", true);
            jumpCount = 0;
        }
    }


    void Death()
    {
        animator.SetBool("death", true);
    }

    void PlayAudioDeath()
    {
        audioBase.clip = audioDeath;
        audioBase.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(wallCheckers[0].position, wallCheckersRadius);
        Gizmos.DrawWireSphere(wallCheckers[1].position, wallCheckersRadius);
    }
    void PlayAudioAttack()
    {
        audioBase.PlayOneShot(audioAttack);
    }

    void PlayAudioWalk()
    {
        audioBase.clip = audioWalk;
        audioBase.Play();
    }
    void Jump()
    {
        
        if (IsGrounded())
        {
            animator.SetBool("jump", false);
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {   
            if (IsGrounded() || jumpCount < maxJumpCount - 1)
            {
                
                audioBase.PlayOneShot(audioJump);
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
                jumpCount++;
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }

    void AttackVerify()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            audioBase.clip = audioDamage;
            audioBase.Play();
            life -= 10;
            lifeBar.value -= life;
            animator.SetTrigger("hit");
        }
    }
        private IEnumerator DisableMovement(float time)
    {
        rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(time);
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    bool IsOnWall()
    {
        isOnWall = Physics2D.OverlapCircle(wallCheckers[0].position, wallCheckersRadius, groundLayer) || 
                   Physics2D.OverlapCircle(wallCheckers[1].position, wallCheckersRadius, groundLayer);

        if (isOnWall)
        {
            wallJumpDirection = wallCheckers[0].position.x - wallCheckers[1].position.x;
        }

        return isOnWall;
    }

   
}

/*using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] wallCheckers = new Transform[2];
    [SerializeField] private float wallCheckersRadius = 0.2f;
    public float speed = 3f;
    public float jumpForce = 5f;
    public int maxJumpCount = 2;
    public int life;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private AudioSource audioBase;
    public AudioClip audioDeath;
    public AudioClip audioDamage;
    public AudioClip audioAttack;
    public AudioClip audioJump;
    public AudioClip audioWalk;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    private Rigidbody2D rig;
    private Animator animator;
    public GameObject[] heart = new GameObject[3];
    private bool isJumping;
    private int jumpCount;

    enum PlayerStates
    {
        IDLE,
        WALK,
        JUMP,
        FALL,
        WALLSLIDE,
        WALLJUMP,
        ATTACK,
        DEATH    
    };
    private PlayerStates currentState = PlayerStates.IDLE;
    
    void Start()
    {
        life = valueManagement.life;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioBase = GetComponent<AudioSource>();
    }


    void Update()
    {
        switch (currentState)
        {
            case PlayerStates.IDLE: Idle();break;
            case PlayerStates.WALK: Walk();break;
            case PlayerStates.JUMP: Jump();break;
            case PlayerStates.FALL: Fall(); break;
            case PlayerStates.WALLSLIDE: WallSlide();break;
            case PlayerStates.WALLJUMP: WallJump();break;
            case PlayerStates.ATTACK: Attack();break;
            case PlayerStates.DEATH: Death();break;
        }

    }

    void Idle(){
        rig.velocity = new Vector2(0f, 0f);
        jumpCount = 0;
        animator.SetBool("walk", false);
        animator.SetBool("jump", false);
        animator.SetBool("idle", true);
        animator.SetBool("death", false);
        currentState = CheckStateIdle();
    }
    private PlayerStates CheckStateIdle()
    {
        if (Input.GetAxis("Horizontal") != 0 && IsGrounded()){
            return PlayerStates.WALK;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded()){
            return PlayerStates.JUMP;
        }

        return PlayerStates.IDLE;

    }

    void Walk()
    {
        float direction = Input.GetAxis("Horizontal");
        jumpCount = 0;
        rig.velocity = new Vector2(direction*speed, rig.velocity.y);
        currentState = CheckStateWalk();
    }

    private PlayerStates CheckStateWalk()
    {
        if (Input.GetAxis("Horizontal") == 0){
            return PlayerStates.IDLE;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded()){
            return PlayerStates.JUMP;
        }
        return PlayerStates.IDLE;

    }

    void Jump() 
    {
        float direction = Input.GetAxis("Horizontal");
        if (jumpCount < 1)
        {
            rig.velocity = new Vector2(direction*speed, jumpForce);
            jumpCount += 1;
        }
        
        currentState = CheckStateJump();

    }
    private PlayerStates CheckStateJump()
    {
        if(rig.velocity.y < 0 )
        {
            return PlayerStates.FALL;
        }
        return PlayerStates.JUMP;
    }

    void Fall(){
        float direction = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(direction*speed, rig.velocity.y);
        currentState = CheckStateFall();
    }
    private PlayerStates CheckStateFall()
    {
        if(IsGrounded())
        {
            return PlayerStates.IDLE;
        }
            return PlayerStates.FALL;
    }

    void Attack(){

    }

    void WallSlide(){

    }
    
    void WallJump(){

    }
    void Death(){

    }


    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }



}*/