using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Config")]

    private PlayerAnimations playerAnimations;

    private float health;
    [SerializeField] private int maxHealth;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        ResetHealth();
    }
    
    public void TakeDamage(float amount)
    {
        if (health <= 0) return;
        
        health -= amount;
        
        if(health <= 0f)
        {
            health = 0f;
            PlayerDeath();
        }
    }
    
    private void PlayerDeath()
    {
        playerAnimations.SetDeadAnimation();
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
    
    
}
