using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossShootPoint : MonoBehaviour
{
    public GameObject boss;
    public GameObject projectile;
    public float TimeBetweenInstantiate;
    private float _TimeBetweenInstantiate;
    private Animator bossAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        bossAnimator = boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_TimeBetweenInstantiate <= 0)
        {
            if (bossAnimator.GetBool("SecondAttack")&&bossAnimator.GetCurrentAnimatorStateInfo(0).IsTag("SecondAttack"))
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(0,0,270));
                _TimeBetweenInstantiate = TimeBetweenInstantiate;
            }
        }
        else
        {
            _TimeBetweenInstantiate -= Time.deltaTime;
        }
    }
}
