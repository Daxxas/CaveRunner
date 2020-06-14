using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using characterState;

public class PlayerController : CharacterController
{

    private PlayerInputEvents playerInput;
    
    private float velocityXSmoothing;
    private bool reachedApex = true;
    private float gravity => -2 * MaxjumpHeight / (timeJumpApex * timeJumpApex);
    private float MaxJumpVelocity => 2 * MaxjumpHeight / timeJumpApex;
    private float MinJumpVelocity => Mathf.Sqrt(2 * Mathf.Abs(gravity) * MinjumpHeight);
    private float maxHeightReached = Mathf.NegativeInfinity;
    
    public override void Start()
    {
        base.Start();
        controller = GetComponent<Controller2D>();
        playerInput = GetComponent<PlayerInputEvents>();
        characterState = CharacterState.IDLE;
    }

    void Update()
    {
        this.attack = playerInput.Attack;
        
        if (!(velocity.y > 0 && !controller.collisions.below))
        {
            characterState = CharacterState.IDLE;
        }

        CalculateVelocity();
        
        if (canWallJump)
        {
            wallDirX = (controller.collisions.left) ? -1 : 1;
            
            WallSlideHandling();
        }

        JumpHandling();

        controller.Move(velocity * Time.deltaTime, playerInput.Input);
        
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

    private void CalculateVelocity()
    {
        if (canMove)
        {
            float targetVelocityX = playerInput.Input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
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

        if (Mathf.Abs(velocity.x) > 10)
        {
            characterState = CharacterState.MOVE;
        }

        if (velocity.y < 0 && !controller.collisions.below)
        {
            characterState = CharacterState.FALL;
        }
    }
    
    private void WallSlideHandling()
    {
        wallSliding = false;
        if(autoWallSlide) {
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
            {
                wallSliding = true;
                characterState = CharacterState.WALLSLIDE;

                if (velocity.y < -wallSlideSpeedMax)
                {
                    velocity.y = -wallSlideSpeedMax;
                }

                if (timeToWallUnStick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;
                    if(Mathf.RoundToInt(playerInput.Input.x) != wallDirX && Mathf.RoundToInt(playerInput.Input.x) != 0)
                    {
                        timeToWallUnStick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnStick = wallStickTime;
                    }
                }
                else
                {
                    timeToWallUnStick = wallStickTime;
                }
            }
        }
        else
        {
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && (Mathf.RoundToInt(playerInput.Input.x) == wallDirX || timeToWallUnStick > 0))
            {
                wallSliding = true;
                characterState = CharacterState.WALLSLIDE;

                if (velocity.y < -wallSlideSpeedMax)
                {
                    velocity.y = -wallSlideSpeedMax;
                }

                if (timeToWallUnStick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;
                    if(Mathf.RoundToInt(playerInput.Input.x) != wallDirX)
                    {
                        timeToWallUnStick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnStick = wallStickTime;
                    }
                }
                else
                {
                    timeToWallUnStick = wallStickTime;
                }
            }
        }
    }
    
    private void JumpHandling()
    {
        if (playerInput.JumpPress)
        {
            characterState = CharacterState.JUMP;
            
            if(controller.collisions.below)
            {
                if (controller.collisions.slidingDownMaxSlope)
                {
                    if (Mathf.RoundToInt(playerInput.Input.x) != -Mathf.Sign(controller.collisions.slopeNormal.x))
                    {
                        velocity.y = MaxJumpVelocity * controller.collisions.slopeNormal.y;
                        velocity.x = MaxJumpVelocity * controller.collisions.slopeNormal.x;
                    }
                }
                else
                {
                    Jump();
                }
                
            }

            if (wallSliding)
            {
                if (Mathf.RoundToInt(playerInput.Input.x) == wallDirX)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (Mathf.RoundToInt(playerInput.Input.x) == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallJumpLeap.x;
                    velocity.y = wallJumpLeap.y;
                }
            }
        }
        
        if (playerInput.JumpRelease)
        {
            if (velocity.y > MinJumpVelocity)
            {
                velocity.y = MinJumpVelocity;
            }
        }
        
        if (!reachedApex && maxHeightReached > transform.position.y)
        {
            reachedApex = true;
        }
    }

    private void Jump()
    {
        FindObjectOfType<SoundManager>().Play("Jump");
        velocity.y = MaxJumpVelocity;
        reachedApex = false;
        maxHeightReached = Mathf.Max(transform.position.y, maxHeightReached);
    }

    
}
