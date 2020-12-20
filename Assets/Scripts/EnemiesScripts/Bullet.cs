using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public bool destroyableBullet;
    public float speed = 2f;
    public GameObject shooter;
    private Rigidbody2D _rigidbody;
    public LayerMask groundLayer;
    public int Damage;
    public bool enemyBullet;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Physics2D.Raycast(transform.position, direction, -0.1f, groundLayer))
        {
            AddDamage();
        }
    }
    private void FixedUpdate()
    {
        Vector2 movement = direction.normalized * speed;
        _rigidbody.velocity = movement;
    }
    public void AddDamage()
    {
        if(destroyableBullet==true)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& collision.name == "Hurtbox" && enemyBullet)
        {
            AddDamage();
            collision.SendMessageUpwards("AddDamage", Damage);
        }

        if (collision.CompareTag("Enemy") && enemyBullet==false)
        {
            collision.SendMessageUpwards("AddDamage", Damage);
        }
    }
}
