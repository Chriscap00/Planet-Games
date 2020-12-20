using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAttackController : MonoBehaviour
{
    public float delayBetweenAttacks;

    private Animator _animator;
    private bool _seeingLeft;
    private bool _seeingRight;
    private Rigidbody2D _rigidBody;
    private float _delayBetweenAttacks;
    private float _scale;
    private bool _seeingLeftTD;

    private bool _isAttacking;
    // Start is called before the first frame update
    void Awake()
    {
        _scale = transform.localScale.x;
        _delayBetweenAttacks = delayBetweenAttacks;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        if (gameObject.transform.localScale.x < 0)
        {
            _seeingLeft = false;
            _seeingRight = true;
        }
        else if (gameObject.transform.localScale.x > 0)
        {
            _seeingLeft = true;
            _seeingRight = false;
        }
        _seeingLeftTD = _seeingLeft;
    }

    private void OnEnable()
    {
        _scale = transform.localScale.x;
        if(_seeingLeftTD)
        {
            _seeingLeft = true;
            _seeingRight = false;
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            _seeingLeft = false;
            _seeingRight = true;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Update()
    {
        delayBetweenAttacks -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player") && collision.transform.position.x < transform.position.x && _seeingLeft == false)
            {
                flip();
                _seeingLeft = true;
                _seeingRight = false;
            }
            else if (collision.CompareTag("Player") && collision.transform.position.x > transform.position.x && _seeingRight == false)
            {
                flip();
                _seeingLeft = false;
                _seeingRight = true;
            }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (delayBetweenAttacks <= 0)
        {

                if (collision.CompareTag("Player") && collision.transform.position.x < transform.position.x && _seeingLeft == false)
                {
                    flip();
                    _seeingLeft = true;
                    _seeingRight = false;
                }
                else if (collision.CompareTag("Player") && collision.transform.position.x > transform.position.x && _seeingRight == false)
                {
                    flip();
                    _seeingLeft = false;
                    _seeingRight = true;
                }
                if (collision.CompareTag("Player"))
                {
                    Attack();
                }
        }


    }

    private void Attack()
    {
        _isAttacking = true;
        _animator.SetBool("Attack", true);
        delayBetweenAttacks = _delayBetweenAttacks;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool("Attack", false);
        _isAttacking = false;
    }

    private void flip()
    {
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX*-1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

}
