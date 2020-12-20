﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject shooter;
    private Transform _firePoint;

    void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
    
    //this function shoot a bullet prefab

    public void Shoot()
    {
        if (bulletPrefab != null && _firePoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;

            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if (shooter.transform.localScale.x < 0f)
            {
                // Left
                bulletComponent.direction = Vector2.left; // new Vector2(-1f, 0f)
            }
            else
            {
                // Right
                bulletComponent.direction = Vector2.right; // new Vector2(1f, 0f)
            }
        }
    }

}
