//using System.Numerics;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class pig : MonoBehaviour
{
    public GameObject tiro;
    public Player player;
    public GameObject posTiro;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void directionChanger(){
       float posRelative = getDistance();
        if(posRelative>=0){
            transform.rotation = Quaternion.Euler(0,0,0);
            tiro.GetComponent<box>().direction = -1;
        }else{
            transform.rotation = Quaternion.Euler(0,180,0);
            tiro.GetComponent<box>().direction = 1;
        }

    }

    void throwCondition(){
         float posRelative = getDistance();
         if ( Mathf.Abs(posRelative) <=5f){
            animator.SetBool("canAttack", true);
         }else{
            animator.SetBool("canAttack", false);
         }
    }

    float getDistance(){ 
        float posRelative = this.transform.position.x - player.transform.position.x;
        return posRelative;
    }
    void Update()
    {
        directionChanger();
        throwCondition();
    }
    void atirar(){
        //PlayerVar.GetComponent<Player>().life += 1
         Instantiate(tiro, posTiro.transform.position, Quaternion.identity);

    }
}
