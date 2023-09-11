using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : EnemyBase
{
    // Start is called before the first frame update
    private Player player;
    [SerializeField] private GameObject fireball;
    [SerializeField] private float minDist;
    [SerializeField] private int fireballSpeed;
    [SerializeField] private Transform fireballPoint;
    void Start()
    {
        player = FindObjectOfType<Player>();
        animatorControll = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionChanger();
        if(Vector2.Distance(player.transform.position, transform.position) < minDist){
            animatorControll.SetBool("Attacking", true);

        }else{
            animatorControll.SetBool("Attacking", false);
        }
        
    }

    override public void Attack(){
        GameObject newFireball = Instantiate(fireball, fireballPoint.position, Quaternion.identity);
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        newFireball.GetComponent<Rigidbody2D>().velocity = directionToPlayer * fireballSpeed;
    }

   override public void Spawn(){
        
    }

    override public void Death(){

    }

    override public void movimentation(){

    }

    void DirectionChanger(){
       float posRelative = GetDistance();
        if(posRelative>=0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }else{
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    float GetDistance(){
        float posRelative = 0;
        if (player != null){
            posRelative = this.transform.position.x - player.gameObject.transform.position.x;
        }
        return posRelative;
    }

}
