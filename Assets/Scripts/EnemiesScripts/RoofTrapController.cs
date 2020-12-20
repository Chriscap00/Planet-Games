using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTrapController : MonoBehaviour
{
    public Sprite buttonOn;
    public Sprite buttonOff;
    public GameObject trap;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidBody;
    private RigidbodyConstraints2D _constrait;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = buttonOn;
        _rigidBody = trap.GetComponent<Rigidbody2D>();
        _constrait = _rigidBody.constraints;
        freezeConstraits();
    }
    private void OnDisable()
    {
        freezeConstraits();
        _renderer.sprite = buttonOn;
    }
    private void freezeConstraits()
    {
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _boxCollider.enabled=false;
            _renderer.sprite = buttonOff;
            _rigidBody.constraints = _constrait;
        }
    }
}
