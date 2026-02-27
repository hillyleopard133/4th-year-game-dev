using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private Transform[] attackPositions;
    
    [Header("Melee Config")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;
    [SerializeField] private LayerMask enemyLayer;
    
    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;

    private Transform currentAttackPosition;
    private float currentAttackRotation;

    private float attackDamage;

    private void Awake()
    {
        actions = new PlayerActions();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        
    }

    private void Start()
    {
        actions.Attack.Attack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetAttackDirection();
    }
    
    private void Attack()
    {
        
        attackCoroutine = StartCoroutine(IEAttack());
    }

    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null)
        {
            yield break;    
        }

        MeleeAttack();
        
        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
    }

    private void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();
        
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, minDistanceMeleeAttack, enemyLayer);
        foreach (Collider2D enemy in enemiesInRange)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    private void GetAttackDirection()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;
        switch (moveDirection.x)
        {
            case > 0f:  //Right
                currentAttackPosition = attackPositions[1];
                break;
            case < 0f :  //Left
                currentAttackPosition = attackPositions[3];
                break;
        }
        
        switch (moveDirection.y)
        {
            case > 0f:  //Up
                currentAttackPosition = attackPositions[0];
                break;
            case < 0f :  //Down
                currentAttackPosition = attackPositions[2];
                break;
        }
        
    }

    private void OnEnable()
    {
        actions.Attack.Enable();
    }

    private void OnDisable()
    {
        actions.Attack.Disable();
    }

}