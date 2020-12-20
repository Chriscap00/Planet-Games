using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firstbossbehavior : MonoBehaviour
{
    public int passToSecondPhase;
    public int maxHealth;
    public bool secondPhase = false;
    public GameObject _hurtBox;
    public GameObject _hitBox;
    public GameObject _shootPoint;
    public float timeBetweenAttacks;
    public float TopAttackTime;
    public bool leftCorner;
    public int attacksPerCorner;
    public GameObject leftCornerPoint;
    public GameObject rightCornerPoint;
    public GameObject topLeftCornerPoint;
    public GameObject topRightCornerPoint;
    public GameObject roofTopPoint;
    public int changeOport;
    public int changePoints;
    public float TimeBetweenAttacksSecondPhase;
    public GameObject[] thinksToActivateAfterDie;
    [SerializeField]
    private int changeOportNum;
    private float _TopAttackTime;
    private float _timeBetweenAttacks;
    private BossHealth enemyHScript;
    private int Health;
    private Animator animator;
    private bool initLeftCorner;
    private Transform _initPos;
    private GameObject[] bulletsToDeactivate;
    private int iteratNum = 0;

    private void Awake()
    {
        initLeftCorner = leftCorner;
        _initPos = GetComponent<Transform>();
        enemyHScript = GetComponent<BossHealth>();
        changeOportNum = changeOport;
        _TopAttackTime = TopAttackTime;
        animator = GetComponent<Animator>();
        if (leftCorner)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.position = new Vector3(leftCornerPoint.transform.position.x, leftCornerPoint.transform.position.y, leftCornerPoint.transform.position.z);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gameObject.transform.position = new Vector3(rightCornerPoint.transform.position.x, rightCornerPoint.transform.position.y, rightCornerPoint.transform.position.z);
        }
    }

    private void Update()
    {
        if(enemyHScript.Health> passToSecondPhase)
        {
            firstBossPhase();
        }
        else
        {
            secondBossPhase();
        }
    }
    private void secondBossPhase()
    {
        timeBetweenAttacks = TimeBetweenAttacksSecondPhase;
        if (changeOportNum <= 0)
        {
            gameObject.transform.position = new Vector3(roofTopPoint.transform.position.x, roofTopPoint.transform.position.y, roofTopPoint.transform.position.z);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Attack", false);
            animator.SetBool("SecondAttack", true);
            if (_TopAttackTime > 0)
            {
                animator.SetFloat("SecondAttackTime", _TopAttackTime);
                _TopAttackTime -= Time.deltaTime;
            }
            else
            {
                _TopAttackTime = TopAttackTime;
                changeOportNum = changeOport;
                _timeBetweenAttacks = 0;
                animator.SetBool("SecondAttack", false);
                animator.SetBool("Attack", false);
            }
        }
        else
        {

            if (_timeBetweenAttacks <= 0)
            {
                animator.SetBool("Attack", false);
                _timeBetweenAttacks = timeBetweenAttacks;
                switch (changePoints)
                {
                    case 1:
                        {
                            changeOportNum -= 1;
                            secondPhase = false;
                            transform.localScale = new Vector3(1, 1, 1);
                            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                            gameObject.transform.position = new Vector3(leftCornerPoint.transform.position.x, leftCornerPoint.transform.position.y, leftCornerPoint.transform.position.z);
                            changePoints += 1;
                            break;
                        }
                    case 2:
                        {
                            changeOportNum -= 1;
                            secondPhase = false;
                            transform.localScale = new Vector3(-1, 1, 1);
                            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                            gameObject.transform.position = new Vector3(rightCornerPoint.transform.position.x, rightCornerPoint.transform.position.y, rightCornerPoint.transform.position.z);
                            changePoints += 1;
                            break;
                        }
                    case 3:
                        {
                            changeOportNum -= 1;
                            secondPhase = true;
                            transform.localScale = new Vector3(1, 1, 1);
                            gameObject.transform.rotation = Quaternion.Euler(0, 0, -35.416f);
                            gameObject.transform.position = new Vector3(topLeftCornerPoint.transform.position.x, topLeftCornerPoint.transform.position.y, topLeftCornerPoint.transform.position.z);
                            changePoints += 1;
                            break;
                        }
                    case 4:
                        {
                            changeOportNum -= 1;
                            secondPhase = true;
                            transform.localScale = new Vector3(-1, 1, 1);
                            gameObject.transform.rotation = Quaternion.Euler(0, 0, 35.416f);
                            gameObject.transform.position = new Vector3(topRightCornerPoint.transform.position.x, topRightCornerPoint.transform.position.y, topRightCornerPoint.transform.position.z);
                            changePoints = 1;
                            break;
                        }
                }
            }
            else
            {
                _timeBetweenAttacks -= Time.deltaTime;
                animator.SetBool("Attack", true);
                if (_timeBetweenAttacks <= 0.067f)
                {
                    animator.SetBool("Attack", false);
                    animator.Play("BossVanishEffectAnimation");
                }
            }
        }
    }


    private void firstBossPhase()
    {
        Health = enemyHScript.Health;
        if (changeOportNum <= 0)
        {
            gameObject.transform.position = new Vector3(roofTopPoint.transform.position.x, roofTopPoint.transform.position.y, roofTopPoint.transform.position.z);
            animator.SetBool("Attack", false);
            animator.SetBool("SecondAttack", true);
            if (_TopAttackTime > 0)
            {
                animator.SetFloat("SecondAttackTime", _TopAttackTime);
                _TopAttackTime -= Time.deltaTime;
            }
            else
            {
                _TopAttackTime = TopAttackTime;
                changeOportNum = changeOport;
                _timeBetweenAttacks = 0;
                animator.SetBool("SecondAttack", false);
                animator.SetBool("Attack", false);
            }
        }
        else
        {

            if (_timeBetweenAttacks <= 0)
            {
                animator.SetBool("Attack", false);
                _timeBetweenAttacks = timeBetweenAttacks;
                leftCorner = !leftCorner;

                if (leftCorner)
                {
                    changeOportNum -= 1;
                    transform.localScale = new Vector3(1, 1, 1);
                    gameObject.transform.position = new Vector3(leftCornerPoint.transform.position.x, leftCornerPoint.transform.position.y, leftCornerPoint.transform.position.z);
                }
                else
                {
                    changeOportNum -= 1;
                    transform.localScale = new Vector3(-1, 1, 1);
                    gameObject.transform.position = new Vector3(rightCornerPoint.transform.position.x, rightCornerPoint.transform.position.y, rightCornerPoint.transform.position.z);
                }
            }
            else
            {
                _timeBetweenAttacks -= Time.deltaTime;
                animator.SetBool("Attack", true);
                if (_timeBetweenAttacks <= 0.067f)
                {
                    animator.SetBool("Attack", false);
                    animator.Play("BossVanishEffectAnimation");
                }
            }

        }
    }
    private void OnDisable()
    {
        leftCorner = initLeftCorner;
        if (leftCorner)
        {
            changeOportNum -= 1;
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.position = new Vector3(leftCornerPoint.transform.position.x, leftCornerPoint.transform.position.y, leftCornerPoint.transform.position.z);
        }
        else
        {
            changeOportNum -= 1;
            transform.localScale = new Vector3(-1, 1, 1);
            gameObject.transform.position = new Vector3(rightCornerPoint.transform.position.x, rightCornerPoint.transform.position.y, rightCornerPoint.transform.position.z);
        }
        _TopAttackTime = TopAttackTime;
        changeOportNum = changeOport;
        _timeBetweenAttacks = 0;
        animator.SetBool("SecondAttack", false);
        enemyHScript.Health = enemyHScript._maxHealth;
        enemyHScript.healthBar.setMaxBarValue(enemyHScript._maxHealth);
        animator.SetBool("Attack", false);
        bulletsToDeactivate = GameObject.FindGameObjectsWithTag("Bullet");
        for (int bulletToDeactivate = 0; bulletToDeactivate < bulletsToDeactivate.Length; bulletToDeactivate++)
        {
            GameObject bullet;
            bullet = bulletsToDeactivate[bulletToDeactivate];
            Destroy(bullet);
        }
        _hurtBox.SetActive(true);
        _hitBox.SetActive(true);
        _shootPoint.SetActive(true);
    }

    private void deactivateBullets()
    {
        foreach (GameObject count in bulletsToDeactivate)
        {
            iteratNum = iteratNum + 1;
            count.SetActive(false);
        }
        iteratNum = 0;
    }

    private void activateAfterDie()
    {
        foreach (GameObject count in thinksToActivateAfterDie)
        {
            iteratNum = iteratNum + 1;
            count.SetActive(true);
        }
    }
}
