using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementController2 : MonoBehaviour
{
    [Header("Enemie Movement")]
    public float speed = 1f;
    [Space(10)]
    [Header("How the enemie will move")]
    [Space(10)]
    public bool enemy=true;
    public bool infiniteFlipLoop=true;
    public int LoopTimes;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public bool facingRight;
    public bool ignoreWall;
    [Space(10)]
    [Header("Waiting Time")]
    [Space(10)]
    public float waitTime;
    public bool randomWaitTime;

    [Space(10)]
    [Header("Regain character")]
    [Space(10)]
    [SerializeField]
    private GameObject _hitBox;
    [SerializeField]
    private GameObject _hurtBox;

    private int _LoopTimes;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private bool _facingRight;
    private Vector2 OldDirection;
    private Vector2 direction;
    private float _oldSpeed;
    private float RandomWaitTime;
    private SpriteRenderer _spriteRenderer;
    private Color _color;
    private Animator _animator;
    private Vector3 _initialPosition;
    private EnemyHealth _enemyHealth;

    void Awake()
    {
        _LoopTimes = LoopTimes;
        _initialPosition = transform.position;
        _oldSpeed = speed;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _facingRight = facingRight;
        _enemyHealth = GetComponent<EnemyHealth>();
        switch (_facingRight)
        {
            case true:
                transform.localScale = new Vector3(-1, 1, 1);
                _facingRight = true;
                break;
            case false:
                transform.localScale = new Vector3(1, 1, 1);
                _facingRight = false;
                break;
        }

        if (speed <= 0)
        {
            _animator.SetBool("Idle", true);
        }
    }
    private void OnDisable()
    {
        transform.position = _initialPosition;
        _facingRight = facingRight;
        switch (_facingRight)
        {
            case true:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case false:
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
        _hitBox.SetActive(true);
        _hurtBox.SetActive(true);
        _enemyHealth.Health = _enemyHealth._maxHealth;
        RegainMovementSpeed();
    }

    private void OnEnable()
    {
        _LoopTimes = LoopTimes;
        if (speed <= 0)
        {
            _animator.SetBool("Idle", true);
        }
    }

    public void FreezeMovement()
    {
        speed = 0;
    }
    public void RegainMovementSpeed()
    {
        speed = _oldSpeed;
    }
    void Update()
    {
        if (infiniteFlipLoop == false && _LoopTimes <= 0)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = Vector2.right;

        if (_facingRight == false)
        {
            direction = Vector2.left;
        }

        if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
        {
            FlipAndWait();
        }

        if(enemy&& _enemyHealth.Health <= 0)
        {
            direction = Vector2.zero;
            speed = 0;
            StopCoroutine("waitingTime");
        }
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = speed;
        if (infiniteFlipLoop==false&&_LoopTimes<=0)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (_facingRight == false)
            {
                horizontalVelocity = horizontalVelocity * -1f;
            }

            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }
    }

    private void FlipAndWait()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        StartCoroutine("waitingTime");
    }
    private IEnumerator waitingTime()
    {
        _animator.SetBool("Idle", true);
        if (randomWaitTime)
        {
            RandomWaitTime = Random.Range(0, waitTime);
        }
        RandomWaitTime = waitTime;
        speed = 0;
        yield return new WaitForSeconds(RandomWaitTime);
        speed = _oldSpeed;
        _LoopTimes -= 1;
        _animator.SetBool("Idle", false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            FlipAndWait();
        }
    }
}
