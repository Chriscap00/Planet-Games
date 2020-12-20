using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int _scytheDamage;
    public int _swordDamage;
    public int specialAttackMeterMax;
    public HealthBar specialAttackBar;
    [HideInInspector]
    public int specialAttackMeter;

    private bool _isAttacking;
    private Animator _animator;
    private bool _scythe;
    private int DamagedCount=1;
    private PlayerController _playerController;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        specialAttackMeter = 0;
        specialAttackBar.setMaxBarValue(specialAttackMeterMax);
        specialAttackBar.setValue(specialAttackMeter);
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _isAttacking = false;
    }

    public void sumSAttackMeter()
    {
        if (specialAttackMeter < specialAttackMeterMax)
        {
            specialAttackMeter += 1;
            specialAttackBar.setValue(specialAttackMeter);
            Debug.Log(specialAttackMeter);
        }
    }

    public void falseAttackInDeath()
    {
        _isAttacking = false;
    }
    private void setAttack()
    {
        if (_isAttacking==false)
        {
            DamagedCount = 1;
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
    }
    //Did you hit an enemy?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool _IsScythe;
        _IsScythe = _playerController.showScythe();

        if (collision.CompareTag("Enemy") && _isAttacking && DamagedCount>0)
        {
            _enemyHealth = collision.GetComponent<EnemyHealth>();
            switch (_IsScythe)
            {
                case false:
                    collision.SendMessageUpwards("AddDamage", _swordDamage);
                    break;
                case true:
                    collision.SendMessageUpwards("AddDamage", _scytheDamage);
                    cinemachineShake.Instance.shakeCamera(1.4f, 0.1f);
                    break;
            }
            DamagedCount -= 1;
            sumSAttackMeter();
        }

        if (collision.CompareTag("Bullet") && _isAttacking && _IsScythe==false)
        {
            Debug.Log("I destroyed a bullet");
            collision.SendMessageUpwards("AddDamage");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DamagedCount = 1;
        }
    }

}
