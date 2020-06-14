using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

enum EnemyType {RANDOM, FLY, GOBLIN, MUSHROOM}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float playerCheckRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private EnemyList enemyList;
    public Vector3 Position => transform.position;
    private int rand;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (enemyType)
        {
            case EnemyType.RANDOM :
                rand = Random.Range(0, enemyList.enemies.Count);
                break;
            case EnemyType.FLY :
                rand = 0;
                break;
            case EnemyType.GOBLIN:
                rand = 1;
                break;
            case EnemyType.MUSHROOM :
                rand = 2;
                break;
            default:
                rand = Random.Range(0, enemyList.enemies.Count);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D playerInRadius = Physics2D.OverlapCircle(transform.position, playerCheckRadius, whatIsPlayer);

        if (playerInRadius)
        {
            Instantiate(enemyList.enemies[rand], transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
