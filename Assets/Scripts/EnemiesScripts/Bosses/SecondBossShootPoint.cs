using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossShootPoint : MonoBehaviour
{
    public GameObject projectile;
    public float TimeBetweenInstantiate;
    private float _TimeBetweenInstantiate;
    private int _anguleNum;
    private Bullet _bullet;
    private void Awake()
    {
        _bullet = projectile.GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_TimeBetweenInstantiate <= 0)
        {
            _anguleNum = Random.Range(1, 5);
            switch (_anguleNum)
            {
                case 1:

                    break;

                case 2:

                    break;

                case 3:
                    Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, -270));
                    _bullet.direction = new Vector2(0, -1);
                    _TimeBetweenInstantiate = TimeBetweenInstantiate;
                    break;

                case 4:

                    break;

                case 5:

                    break;
            }
            _TimeBetweenInstantiate = TimeBetweenInstantiate;
        }
        else
        {
            _TimeBetweenInstantiate -= Time.deltaTime;
        }
    }
}
