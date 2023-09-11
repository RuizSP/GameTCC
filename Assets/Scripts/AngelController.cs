using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelController : MonoBehaviour
{
    private Player player;
    private Animator animator;
    [SerializeField] GameObject fireball;
    [SerializeField] private float fireballSpeed = 5f;
    [SerializeField] private float timeBetweenShots = 2f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = FindObjectOfType<Player>(); 
        }
        DirectionChanger();

        if (AttackCondition())
        {
            Attacking();
        }
        else
        {
            StopAttacking();
        }
    }

    void DirectionChanger(){
       float posRelative = GetDistance();
        if(posRelative>=0){
            transform.rotation = Quaternion.Euler(0,180,0);
        }else{
            transform.rotation = Quaternion.Euler(0, 0,0);
        }
    }

    float GetDistance(){
        float posRelative = 0;
        if (player != null){
            posRelative = this.transform.position.x - player.gameObject.transform.position.x;
        }
        return posRelative;
    }

    bool AttackCondition()
    {
        float posRelative = GetDistance();
        if ( Mathf.Abs(posRelative) <=5f)
        {
            return true;
        }
        return false;
    }

    void Attacking()
    {
        if (!animator.GetBool("Attacking")) // Check if not already attacking
        {
            animator.SetBool("Attacking", true);
        }
    }

         
    void StopAttacking()
    {
     animator.SetBool("Attacking", false);
    }

    void ThrowFireBall(){
        if (player != null)
        {
            GameObject newFireball = Instantiate(fireball, transform.position, Quaternion.identity);

            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            newFireball.GetComponent<Rigidbody2D>().velocity = directionToPlayer * fireballSpeed;
        }

    }



}
