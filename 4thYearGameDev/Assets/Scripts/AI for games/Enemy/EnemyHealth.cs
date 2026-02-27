using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Config")]
    public float health;
    
    [HideInInspector] public float CurrentHealth { get; private set; }
    
    private Rigidbody2D rb2D;
    private EnemyBrain enemyBrain;
    private EnemyAnimations enemyAnimations;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemyAnimations = GetComponent<EnemyAnimations>();
    }

    private void Start()
    {
        CurrentHealth = health;
        enemyBrain.isAlive = true;
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
    }
    
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            DisableEnemy();
        }
    }

    private void DisableEnemy()
    {
        enemyAnimations.SetDeadAnimation();
        enemyBrain.enabled = false;
        enemyBrain.isAlive = false;
        rb2D.bodyType = RigidbodyType2D.Static;
    }
}
