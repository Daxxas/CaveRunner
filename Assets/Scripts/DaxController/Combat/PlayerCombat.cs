using System;
using System.Collections;
using System.Collections.Generic;
using characterState;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    private PlayerController playerController;

    public float invincibleDuration = 2f;
    private float currentInvincibleTime = 0f;

    [SerializeField] private LevelManager levelManager;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public override void Start()
    {
        base.Start();
        playerController = GetComponent<PlayerController>();
    }

    public void Update()
    {
        ResetState();
        
        if (Time.time >= nextAttackTime)
        {
            if (playerController.Attack)
            {
                FindObjectOfType<SoundManager>().Play("Attack");
                engageAttack = true;
                Attack();
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
    }

    private void Attack()
    {
        if (!isDead)
        {
            playerController.characterState = CharacterState.ATTACK;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + attackPosition * playerController.Controller.collisions.faceDir, attackRange, whatIsEnemy);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (Time.time >= currentInvincibleTime)
        {
            currentInvincibleTime = Time.time + invincibleDuration;
            currentHealth -= damage;
            gotHit = true;
            FindObjectOfType<SoundManager>().Play("Hit");
            if (currentHealth <= 0)
            {
                isDead = true;
                playerController.canMove = false;
                levelManager.DisplayLooseScreen();
            } 
        }
    }

    private IEnumerator Freeze(float duration)
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(duration);
        playerController.canMove = true;
    }

    private void ResetState()
    {
        engageAttack = false;
    }
}
