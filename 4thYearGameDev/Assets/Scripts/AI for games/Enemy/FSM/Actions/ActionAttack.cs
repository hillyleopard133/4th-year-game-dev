using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]
    [SerializeField] public float damage;
    [SerializeField] private float timeBtwAttacks;  //time between attacks

    private EnemyBrain enemyBrain;
    private float timer;

    public bool IsFinished;
    private bool attacking;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    private void Update()
    {
        if(!attacking) return;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            attacking = false;
            IsFinished = true;
            enemyBrain.animations.SetMoveBoolTransition(false);
            PlayerHealth playerHealth = enemyBrain.Player.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
    
    public override void Act()
    {
        timer = timeBtwAttacks;
        IsFinished = false;
        attacking = true;
    }

    public void StopAttack()
    {
        attacking = false;
        IsFinished = true;
    }
}
