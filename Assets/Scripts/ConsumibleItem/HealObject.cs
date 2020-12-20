using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour
{
    public PlayerHealth _playerHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerHealth.Health = _playerHealth._MaxHealth;
            _playerHealth.healthBar.setMaxBarValue(_playerHealth._MaxHealth);
            gameObject.SetActive(false);
        }
    }
}
