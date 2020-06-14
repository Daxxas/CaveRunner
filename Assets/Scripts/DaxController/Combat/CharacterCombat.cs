using System;
using System.Collections;
using System.Collections.Generic;
using characterState;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterCombat : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 3;
    public int attackDamage = 1;
    protected int currentHealth;
    
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected float attackRange = 0.4f;
    [SerializeField] protected Vector3 attackPosition = new Vector3(0.75f, 0, 0);
    [SerializeField] protected float freezeHitDuration = 0.45f;
    [SerializeField] protected float attackRate = 1f;
    public bool gotHit = false;
    public bool isDead = false;
    public float nextAttackTime = 0f;
    public bool engageAttack = false;
    
    
    public virtual void Start()
    {
        currentHealth = maxHealth;
    }
}
