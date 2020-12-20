using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public HealthBar healthBar;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        healthBar.setMaxBarValue(Health);
    }
    override public void AddDamage(int _damage)
    {
        if (Health > 0)
        {
            int damage = _damage;
            Health -= damage;
            healthBar.setValue(Health);
            StartCoroutine("VisualFeedback");
        }
        if (Health <= 0 && died == false)
        {
            died = true;
            hitBox.SetActive(false);
            hurtBox.SetActive(false);
            _animator.SetTrigger("Death");
        }
    }
    private void OnEnable()
    {
        healthBar.setMaxBarValue(_maxHealth);
        hitBox.SetActive(true);
        hurtBox.SetActive(true);
    }

}
