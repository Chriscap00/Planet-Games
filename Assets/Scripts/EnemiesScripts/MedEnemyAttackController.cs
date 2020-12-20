using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedEnemyAttackController : MonoBehaviour
{
    public bool meleeAttackWithoutAnimation;
    public int damage;
    public float delayBetweenAttacks;
    public GameObject[] objectsToActivate;

    private bool _canAttack = false;
    private Animator _animator;
    private GameObject playerCollision;
    private float _delayBetweenAttacks;
    private int iteratNum = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _delayBetweenAttacks = delayBetweenAttacks;
    }
    private void Update()
    {
        delayBetweenAttacks -= Time.deltaTime;
    }
    public void CantAttack()
    {
        _canAttack = false;
    }
    public void CheckPlayerToAttack()
    {
        if (playerCollision != null)
        {
            switch (playerCollision.CompareTag("Player"))
            {
                case true:
                    playerCollision.SendMessageUpwards("AddDamage", damage);
                    playerCollision = null;
                    break;
                case false:
                    break;
            }
        }
    }

    public void canAttack()
    {
        _canAttack = true;
    }

    private void OnDisable()
    {
        foreach (GameObject count in objectsToActivate)
        {
            iteratNum = iteratNum + 1;
            count.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (delayBetweenAttacks <= 0)
        {
            if (collision.CompareTag("Player"))
            {
                _animator.SetBool("Attack", true);
                if (_canAttack)
                {
                    collision.SendMessageUpwards("AddDamage", damage);
                    delayBetweenAttacks = _delayBetweenAttacks;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (delayBetweenAttacks <= 0)
        {
            if (collision.CompareTag("Player"))
            {
                _animator.SetBool("Attack", true);
                if (_canAttack)
                {
                    collision.SendMessageUpwards("AddDamage", damage);
                    delayBetweenAttacks = _delayBetweenAttacks;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool("Attack", false);
    }
}
