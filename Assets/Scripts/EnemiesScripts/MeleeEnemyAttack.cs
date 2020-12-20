using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    public bool meleeAttackWithoutAnimation;
    public int damage;
    public float delayBetweenAttacks;
    private bool _canAttack=true;

    private float _delayBetweenAttacks;
    private void Awake()
    {
        _canAttack = true;
    }
    private void Update()
    {
        delayBetweenAttacks -= Time.deltaTime;
    }
    public void CantAttack()
    {
        _canAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (delayBetweenAttacks <= 0)
        {
            if (collision.CompareTag("Player")&&collision.name=="Hurtbox"&& _canAttack)
            {
                collision.SendMessageUpwards("AddDamage",damage);
                delayBetweenAttacks = _delayBetweenAttacks;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (delayBetweenAttacks <= 0)
        {
           if (collision.CompareTag("Player") && collision.name == "Hurtbox"&& _canAttack)
           {
               collision.SendMessageUpwards("AddDamage", damage);
               delayBetweenAttacks = _delayBetweenAttacks;
           }
        }
    }
}
