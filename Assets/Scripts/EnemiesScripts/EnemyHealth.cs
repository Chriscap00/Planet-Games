using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject hurtBox;
    public int Health;
    public int _maxHealth;
    public float visualFeedbackTime;
    public float stunTime;
    [HideInInspector]
    public bool died;
    public Animator _animator;
    [HideInInspector]
    public SpriteRenderer _renderer;
    private Color _color;
    private void Awake()
    {
        _maxHealth = Health;
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        died = false;
    }
    public virtual void AddDamage(int _damage)
    {
        if(Health>0)
        {
            int damage = _damage;
            Health -= damage;
            StartCoroutine("VisualFeedback");
        }
        if (Health <= 0&& died==false)
        {
            died = true;
            hitBox.SetActive(false);
            hurtBox.SetActive(false);
            _animator.SetTrigger("Death");
        }
    }
    private void OnEnable()
    {
        Health = _maxHealth;
        died = false;
    }
    public IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(visualFeedbackTime);

        _renderer.color = Color.white;
    }

    private void destroyThisThing()
    {
        gameObject.SetActive(false);
        _color=_renderer.color;
        _color.a = 1;
        _renderer.color = _color;
    }
}
