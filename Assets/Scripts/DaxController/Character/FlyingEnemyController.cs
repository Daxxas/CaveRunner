using System;
using System.Collections;
using System.Collections.Generic;
using EnemyBehaviourSpace;
using UnityEditor.UIElements;
using UnityEngine;

public class FlyingEnemyController : EnemyController
{
    public Transform target;
    private Vector2 velocitySmoothing;
    
    public override void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player").transform;
    }

    public void Update()
    {
        if (canMove && target != null)
        {
            Vector2 direction = target.position - transform.position;

            Vector2 targetVelocity = direction.normalized * moveSpeed;
            velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref velocitySmoothing,accelerationTimeAirborne);
            
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
