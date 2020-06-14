using System;
using System.Collections;
using System.Collections.Generic;
using characterState;
using EnemyBehaviourSpace;
using UnityEngine;


public class EnemyController : CharacterController
{
    public EnemyBehaviour enemyBehaviour;
    
    public int walkDirection = 1;
    private float velocityXSmoothing;
    private bool reachedApex = true;
    private float gravity => -2 * MaxjumpHeight / (timeJumpApex * timeJumpApex);
    private float MaxJumpVelocity => 2 * MaxjumpHeight / timeJumpApex;
    private float MinJumpVelocity => Mathf.Sqrt(2 * Mathf.Abs(gravity) * MinjumpHeight);
    private float maxHeightReached = Mathf.NegativeInfinity;
    [Header("Checks")] 
    [SerializeField] private float wallCheckDistance = 1;
    [SerializeField] private float edgeFrontCheckDistance = 1;
    [SerializeField] private float edgeFrontCheckDepth = 3;

    
    
    public override void Start()
    {
        base.Start();
        controller = GetComponent<Controller2D>();
    }

    void Update()
    {
        MakeDecision();

        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime);
    }

    private void CalculateVelocity()
    {
        if (canMove)
        {
            float targetVelocityX = moveSpeed * walkDirection;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
                (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }
        else
        {
            velocity.x = 0;
        }

        if (velocity.y > 0)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * gravityFallScale * Time.deltaTime;
        }
        
        if (controller.collisions.above || (controller.collisions.below && velocity.y <= 0.1f && !controller.collisions.ClimbingSlope))
        {
            if(controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
                characterState = CharacterState.GROUNDSLIDE;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    private void MakeDecision()
    {
        if (WallFrontCheck())
        {
            walkDirection = -walkDirection;
        }
        else if(!EdgeFrontCheck() && controller.collisions.below)
        {
            walkDirection = -walkDirection;
        }
    }
    
    private bool WallFrontCheck()
    {
        RaycastHit2D front = Physics2D.Raycast(transform.position, new Vector3(walkDirection, 0), wallCheckDistance, controller.collisionMask);
        return front;
    }

    private bool EdgeFrontCheck()
    {
        RaycastHit2D front = Physics2D.Raycast(new Vector2(transform.position.x + edgeFrontCheckDistance * walkDirection, transform.position.y), Vector2.down, edgeFrontCheckDepth, controller.collisionMask);
        return front;
    }

    private void OnDrawGizmos()
    {        
        Gizmos.DrawLine(new Vector2(transform.position.x + edgeFrontCheckDistance * walkDirection, transform.position.y), 
                        new Vector2(transform.position.x + edgeFrontCheckDistance * walkDirection, transform.position.y - edgeFrontCheckDepth)); 
    }
}
