using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyBase
{   
    private Rigidbody2D rig;
    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        animatorControll = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animatorControll.GetBool("walk") == true){
            movimentation();
        }
    
    }

    override public void Spawn()
    {

    }
    override public void Attack()
    {

    }
    override public void Death()
    {

    }
    override public void movimentation(){
        var direction = (target.transform.position.x - this.transform.position.x);
        if (direction <0 ){
            rig.velocity = new Vector2(-speed, rig.velocity.y);
            transform.rotation = Quaternion.Euler(0,360, 0);
        }else{
             rig.velocity = new Vector2(speed, rig.velocity.y);
             transform.rotation = Quaternion.Euler(0,180, 0);
        }
        
        
        
    }

    public void SetAnimation(){
        animatorControll.SetBool("walk", true);
    }
}
