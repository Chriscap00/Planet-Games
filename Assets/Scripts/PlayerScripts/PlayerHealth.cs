using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public float InvulnerabilityTime;
    public int _MaxHealth;
    public GameObject gameOverMenu;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _PlayerHurtBox;
    private Color _color;
    public HealthBar healthBar;
    private PlayerController _playerController;
    private Transform _playerTransform;
    private SpawnPoint spawnPoint;
    private int iteratNum = 0;
    private PlayerAttack _playerAttack;
    private bool inmortalMode;

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerTransform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _PlayerHurtBox = GetComponentInChildren<CapsuleCollider2D>();
        _playerController = GetComponent<PlayerController>();
        _color = _spriteRenderer.material.color;
        healthBar.setMaxBarValue(_MaxHealth);
        Debug.Log("Scene");
    }
    public void setHealth(int health)
    {
        Health = health;
    }
    public int getHealth()
    {
        return Health;
    }

    void Start()
    {
        Health = _MaxHealth;
    }
    public void EnaGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    public void RecoveryHealth()
    {
        Health = _MaxHealth;
        healthBar.setValue(Health);
    }

    public void disablePlayer()
    {
        gameObject.SetActive(false);
    }

    public void AddDamage(int damagePoints)
    {
        if (inmortalMode == false)
        {
            Health -= damagePoints;
            healthBar.setValue(Health);
            if (Health >= 1)
            {
                StartCoroutine("VisualFeedback");
            }
            else if (Health <= 0)
            {
                StopAllCoroutines();
                _PlayerHurtBox.enabled = false;
                _animator.SetTrigger("Death");
            }
        }
    }
    public void setInmortalModeFalse()
    {
        inmortalMode = false;
    }

    public void setInmortalModeTrue()
    {
        inmortalMode = true;
    }

    private void OnDisable()
    {
        _playerAttack.falseAttackInDeath();
        spawnPoint = GameObject.Find("SpawnPoint").GetComponent<SpawnPoint>();
        _PlayerHurtBox.enabled = true;
        _animator.enabled = false;
        RecoveryHealth();
        spawnPoint.spawnPlayer();
        GameObject[] enemiesToDesactivate = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemiesToDesactivate.Length);
        foreach (GameObject count in enemiesToDesactivate)
        {
            iteratNum = iteratNum + 1;
            count.SetActive(false);
            if (iteratNum >= (enemiesToDesactivate.Length))
            {
                gameObject.SetActive(false);
            }
        }
        _color.a = 1f;
        _spriteRenderer.material.color = _color;

    }
    private void OnEnable()
    {
        _animator.enabled = true;
        _playerController._scythe = true;
        _playerController.setScythe();
    }

    public void minorHealthRecover()
    {
        Health = Health + 1;
        Health = Mathf.Clamp(Health, 0, 10);
        healthBar.setValue(Health);
    }

    public void onCharge()
    {
        gameOverMenu = GameObject.FindWithTag("gameOverMenu");
        gameOverMenu.SetActive(false);
    }

    private IEnumerator VisualFeedback()
    {
        _color.a = 0.5f;
        _spriteRenderer.material.color = _color;
        _PlayerHurtBox.enabled = false;
        yield return new WaitForSeconds(InvulnerabilityTime);
        _PlayerHurtBox.enabled = true;
        _color.a = 1f;
        _spriteRenderer.material.color = _color;
    }
}
