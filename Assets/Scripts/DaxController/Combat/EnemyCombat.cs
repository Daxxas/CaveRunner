using System;
using System.Collections;
using System.Collections.Generic;
using EnemyBehaviourSpace;
using UnityEngine;


public class EnemyCombat : CharacterCombat
{
    private EnemyController enemyController;
    [SerializeField] private float playerCheckDistance = 1;
    public LayerMask whatIsPlayer;
    private bool isFrozen = false;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (enemyController.enemyBehaviour == EnemyBehaviour.ADVANCED)
        {
            if (CharacterFrontCheck())
            {
                enemyController.canMove = false;
                if (Time.time >= nextAttackTime)
                {
                    engageAttack = true;
                    //Attack function called in animation
                    nextAttackTime = Time.time + (1f / attackRate);
                }
            }
            else if (!isFrozen)
            {
                enemyController.canMove = true;
            }
            
        }
        else if (enemyController.enemyBehaviour == EnemyBehaviour.SIMPLE && !isDead)
        {
            Attack();
        }
    }
    
    private bool CharacterFrontCheck()
    {
        RaycastHit2D front = Physics2D.Raycast(transform.position, new Vector3(enemyController.walkDirection, 0), playerCheckDistance, whatIsPlayer);
        
        return front;
    }

    private void Attack()
    {
        if (!isDead)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + attackPosition * enemyController.Controller.collisions.faceDir, attackRange, whatIsEnemy);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            }
        }
        
    }

    public void TakeDamage(int damage)
    {
        StopAllCoroutines();
        StartCoroutine(Freeze(freezeHitDuration));
        currentHealth -= damage;
        gotHit = true;
        FindObjectOfType<SoundManager>().Play("Hit");
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Freeze(10f));
            isDead = true;
        }
    }

    private IEnumerator Freeze(float duration)
    {
        isFrozen = true;
        enemyController.canMove = false;
        yield return new WaitForSeconds(duration);
        enemyController.canMove = true;
        isFrozen = false;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + attackPosition, attackRange);
    }
}
