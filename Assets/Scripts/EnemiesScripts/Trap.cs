using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    public GameObject _boxcollider;
    public GameObject _roofTrapBoxCollider;
    public float MaxFallSpeed;
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    private Transform _oldTransform;
    private Trap _trap;

    private void Awake()
    {
        _oldTransform = GetComponentInParent<Transform>();
        _transform = GetComponentInParent<Transform>();
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        MaxFallSpeed *= -1;
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.y < MaxFallSpeed)
        {
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, MaxFallSpeed);
        }
    }
    private void OnDisable()
    {
        _transform = _oldTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
        }

        if (collision.CompareTag("Ground"))
        {
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            _roofTrapBoxCollider.SetActive(true);
            _trap = _boxcollider.GetComponent<Trap>();
            _trap.enabled=false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            _roofTrapBoxCollider.SetActive(true);
            _boxcollider.SetActive(false);
            _trap = _boxcollider.GetComponent<Trap>();
            _trap.enabled = false;
        }
    }

}
