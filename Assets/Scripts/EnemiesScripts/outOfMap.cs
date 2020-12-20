using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfMap : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerHealth=collision.GetComponent<PlayerHealth>();
        collision.SendMessageUpwards("AddDamage", _playerHealth._MaxHealth);
    }
}
