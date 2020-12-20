using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PublicVariable
    public float speed = 2f;
    public float dashSpeed = 50;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask grounLayer;
    public float groundCheckRadius;
    public bool AirDash = true;
    public float LongIdleTime = 5f;
    public float DashCooldownTime = 1.3f;
    public float DashTime = 0.1f;
    public bool changeWeapon = true;
    public float MaxFallSpeed;
    public bool _scythe = true;
    public bool facingRight = true;
    public HealthBar specialAttackBar;
    #endregion
    #region privateVariables
    [SerializeField]
    private int comboNum;
    [SerializeField]
    private AudioClip groundFootsteps;
    [SerializeField]
    private AudioClip woodFootsteps;
    [SerializeField]
    private AudioClip scytheAttackAudio;
    [SerializeField]
    private float maxComboDelay = 0.9f;
    [SerializeField]
    private float lastAttackedTime = 0f;
    private bool _Jump;
    private float _DashCooldown = 0f;
    private float _speed;
    private bool _Dash;
    private Vector2 _movement;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded = true;
    private Animator _animator;
    private float _longIdleTimer;
    [SerializeField]
    private bool _isAttacking;
    [SerializeField]
    private bool _canMove = true;
    private bool _changeWeapon = true;
    private bool _moveInSwordCombo = true;
    private AudioSource _audioSource;
    private Color _color;
    private PlayerAttack _playerAttack;

    #endregion
    private void Awake()
    {
        //this awake get three components of the player object, his rigidbody, his sprite renderer and his animator
        _playerAttack = GetComponent<PlayerAttack>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //this start function set the scythe to true, doing that the starting weapong will be the scithe
        _scythe = true;
        _animator.SetBool("Scythe", _scythe);

        ///<sumary>
        ///this set _speed equal to speed to have something like a "backup" 
        ///of the original speed value and not lose it when the player dash
        ///</sumary>
        _speed = speed;
    }

    //this function set the variable canMove to let the player move or not

    public void _cantMove()
    {
        if (_canMove)
        {
            _canMove = false;
        }
        else if (_canMove == false)
        {
            _canMove = true;
        }
    }

    //this will let or not the player move in the sword combo
    private void _cantmoveInSwordCombo()
    {
        if (_moveInSwordCombo)
        {
            _moveInSwordCombo = false;
        }
        else if (_moveInSwordCombo == false)
        {
            _moveInSwordCombo = true;
        }
    }

    public void walkSound()
    {
        float pitchRange;
        pitchRange = UnityEngine.Random.Range(1.2f, 1.45f);
        _audioSource.pitch = pitchRange;
        _audioSource.Play();
    }
    // Update is called once per frame
    private void Update()
    {
        float horizontalinput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalinput, 0f);

        //this variable if the countdown between the dashes
        _DashCooldown += Time.deltaTime;
        //the following two code lines set the mvoement of the player
        float horizontalVelocity = _movement.normalized.x * speed;
        _rigidBody.velocity = new Vector2(horizontalVelocity, _rigidBody.velocity.y);
        //this call every frame the function that set the variable of the ground check
        GroundCkecking();

        //this if, ask if the player can move, if he cant, the movement will be zero 
        if (_canMove)
        {
            if (_moveInSwordCombo == false)
            {
                _rigidBody.velocity = Vector2.zero;
            }

            if(Input.GetButtonDown("specialAttack")&& horizontalinput == 0&& _isAttacking==false)
            {
                if (_playerAttack.specialAttackMeter==_playerAttack.specialAttackMeterMax)
                {
                    if (_scythe)
                    {
                        Debug.Log("scythe special attack");
                        _playerAttack.specialAttackMeter = 0;
                        specialAttackBar.setValue(_playerAttack.specialAttackMeter);
                    }
                    else
                    {
                        _animator.SetBool("playerSwordSpecialAttack", true);
                        _playerAttack.specialAttackMeter = 0;
                        specialAttackBar.setValue(_playerAttack.specialAttackMeter);
                    }
                }
                else
                {
                    Debug.Log("Special meter is not full yet");
                }
            }

            if (Input.GetButtonDown("Dash") && _DashCooldown > DashCooldownTime && horizontalinput != 0)
            {
                if (facingRight)
                {
                    _canMove = false;
                    StartCoroutine("Dash");
                    _DashCooldown = 0f;
                    _Dash = true;
                    _canMove = true;
                }
                else
                {
                    _canMove = false;
                    StartCoroutine("Dash");
                    _DashCooldown = 0f;
                    _Dash = true;
                    _canMove = true;
                }
            }

            if (horizontalinput < 0 && facingRight == true)
            {
                flip();
            }
            if (horizontalinput > 0 && facingRight == false)
            {
                flip();
            }

            if (Input.GetButtonDown("Jump") && _isGrounded && speed <= _speed)
            {
                _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _Jump = true;
            }

            if (Input.GetButtonDown("Fire1") && _Dash == false)
            {
                if (Time.time - lastAttackedTime > maxComboDelay)
                {
                    comboNum = 0;
                }
                if (_scythe == true)
                {
                    _animator.SetTrigger("Attack");
                }
                else if (_scythe == false)
                {
                    lastAttackedTime = Time.time;
                    comboNum++;
                    if (comboNum == 1)
                    {
                        _animator.SetBool("Attack1", true);
                    }
                    comboNum = Mathf.Clamp(comboNum, 0, 3);
                }
            }

            if (Input.GetButtonDown("ChangeWeapon") && _isGrounded && _Dash == false && _isAttacking == false && horizontalinput == 0)
            {
                if (_changeWeapon == true)
                {
                    _changeWeapon = false;
                    _scythe = false;
                    _animator.SetTrigger("ChangeWeapon");
                }
                else if (_changeWeapon == false)
                {
                    _changeWeapon = true;
                    _scythe = true;
                    _animator.SetTrigger("ChangeWeapon");
                }
            }
        }
        else
        {
            _rigidBody.velocity = Vector2.zero;
        }

    }

    public void stop()
    {
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
    }

    public void ScytheAttackSound()
    {
        if (_isAttacking)
        {
            _audioSource.clip = scytheAttackAudio;
            float pitchRange;
            pitchRange = UnityEngine.Random.Range(1f, 1.08f);
            _audioSource.pitch = pitchRange;
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    private void OnDisable()
    {
        _isAttacking = false;
        _canMove = true;
    }

    public void setScythe()
    {
        _animator.SetBool("Scythe", _scythe);
    }
    public bool showScythe()
    {
        return _scythe;
    }

    //if the player pressed twice or more the attack button with the sword, this will start the second attack animation and set the bool of the first attack to false, so it will not repeat
    public void return1()
    {

        if (comboNum >= 2)
        {
            _animator.SetBool("Attack2", true);
            _animator.SetBool("Attack1", false);
        }
        else
        {
            _animator.SetBool("Attack1", false);
        }
    }
    /// <summary>
    /// if the player pressed the attack button three times with the sword, this function will start the third attack 
    /// animation and set the bool of the second attack to false so it will not repeat
    /// </summary>
    public void return2()
    {
        if (comboNum == 3)
        {
            _animator.SetBool("Attack3", true);
        }
        else
        {
            _animator.SetBool("Attack2", false);
        }
    }

    /// <summary>
    /// when the third attack with the sword ends, this function will set all the attack bools to false, to evade any bug in the transitions
    /// </summary>

    public void return3()
    {
        _animator.SetBool("Attack1", false);
        _animator.SetBool("Attack2", false);
        _animator.SetBool("Attack3", false);
        comboNum = 0;
    }

    IEnumerator Dash()
    {
        speed += dashSpeed;
        yield return new WaitForSeconds(DashTime);
        speed -= dashSpeed;
    }
    //this function set the variable is grounded and the variable of the air dash 
    private void GroundCkecking()
    {
        if (_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, grounLayer))
        {
            _isGrounded = true;
            AirDash = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    //do you want to face left/right?
    private void flip()
    {
        facingRight = !facingRight;
        float LocalScaleX = transform.localScale.x;
        LocalScaleX = LocalScaleX * -1;
        transform.localScale = new Vector3(LocalScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void LateUpdate()
    {
        //this will set the idle bool if the movement is zero
        _animator.SetBool("Idle", _movement == Vector2.zero);

        //this will set the isGrounded bool to false or true depending of is the player is in the ground or in the air
        _animator.SetBool("IsGrounded", _isGrounded);

        //this set the vertical velocity in the animator equal to the rigidbody vertical velocity 
        _animator.SetFloat("VerticalVelocity", _rigidBody.velocity.y);

        //this ask if the player pressed the jump button, set the jump trigger in the animator
        if (_Jump == true)
        {
            _animator.SetTrigger("Jump");
            _Jump = false;
        }

        //this ask if the player pressed the button of the dash, set the dash trigger in the animator
        if (_Dash)
        {
            _animator.SetTrigger("Dash");
            _Dash = false;
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            //if the state of the animator is idle, this will add time to the long idle timer
            _longIdleTimer += Time.deltaTime;
            if (_longIdleTimer > LongIdleTime)
            {
                //if the long idle timer is greater than the long idle time, this will set the trigger Long idle
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            //if the state of the animator is not idle, the long idle timer will go back to zero
            _longIdleTimer = 0f;
        }
    }

    private void setAttackingToFalse()
    {
        _isAttacking = false;
    }

    private void FixedUpdate()
    {
        //this will cap the vertical velocity of the player
        if (_rigidBody.velocity.y<MaxFallSpeed)
        {
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, MaxFallSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Did you hit a bullet?

        if(collision.CompareTag("Ground"))
        {
            _audioSource.clip = groundFootsteps;
        }
        if(collision.CompareTag("WoodPlatform"))
        {
            _audioSource.clip = woodFootsteps;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _audioSource.clip = groundFootsteps;
        }
        if (collision.CompareTag("WoodPlatform"))
        {
            _audioSource.clip = woodFootsteps;
        }
    }

    private void SetSwordSpecialAttackFalse()
    {
        _animator.SetBool("playerSwordSpecialAttack", false);
    }

}
