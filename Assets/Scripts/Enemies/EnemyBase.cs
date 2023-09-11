using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float life;
    [SerializeField] protected float poder;
    [SerializeField] protected float speed;
    protected Animator animatorControll;
    
    abstract public  void Spawn();
    abstract public void Attack();
    abstract public void Death();
    abstract public void movimentation();

    
}
