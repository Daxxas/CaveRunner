using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using playerState;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerState playerState;
    
    private PlayerInputEvents playerInput;
    
    [Header("Movement")]
    private Vector3 velocity;
    
    public Vector3 Velocity
    {
        get => velocity;
    }
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float accelerationTimeAirborne = 0f;
    [SerializeField] private float accelerationTimeGrounded = 0f;
    [FormerlySerializedAs("jumpHeight")]
    [Header("Jump")]
    [SerializeField] private float MaxjumpHeight = 4f;
    [SerializeField] private float MinjumpHeight = 1f;
    [SerializeField] private float timeJumpApex = .4f;
    [SerializeField] private float gravityFallScale = 1;
    private float velocityXSmoothing;
    private bool reachedApex = true;
    private float gravity => -2 * MaxjumpHeight / (timeJumpApex * timeJumpApex);
    private float MaxJumpVelocity => 2 * MaxjumpHeight / timeJumpApex;
    private float MinJumpVelocity => Mathf.Sqrt(2 * Mathf.Abs(gravity) * MinjumpHeight);
    private float jumpTimer = 0;
    private float maxHeightReached = Mathf.NegativeInfinity;
    //private bool jumpPress;
    //private bool jumpRelease;
    [Header("Walljump")] 
    [SerializeField] private bool canWallJump = true;
    [SerializeField] private bool autoWallSlide = true;
    private bool wallSliding = false;
    private int wallDirX;
    [SerializeField] private float wallSlideSpeedMax = 6f;
    [SerializeField] private float wallStickTime = 0.25f;
    private float timeToWallUnStick;
    [SerializeField] private Vector2 wallJumpClimb;
    [SerializeField] private Vector2 wallJumpOff;
    [SerializeField] private Vector2 wallJumpLeap;
    

    private Controller2D controller;
    public Controller2D Controller
    {
        get => controller;
    }
    
    void Start()
    {
        controller = GetComponent<Controller2D>();
        playerInput = GetComponent<PlayerInputEvents>();
        playerState = PlayerState.IDLE;
    }

    void Update()
    {
        if (!(velocity.y > 0 && !controller.collisions.below))
        {
            playerState = PlayerState.IDLE;
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
                playerState = PlayerState.GROUNDSLIDE;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    private void CalculateVelocity()
    {
        float targetVelocityX = playerInput.Input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        
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
            playerState = PlayerState.MOVE;
        }

        if (velocity.y < 0 && !controller.collisions.below)
        {
            playerState = PlayerState.FALL;
        }
    }
    
    private void WallSlideHandling()
    {
        wallSliding = false;
        if(autoWallSlide) {
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
            {
                wallSliding = true;
                playerState = PlayerState.WALLSLIDE;

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
                playerState = PlayerState.WALLSLIDE;

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
            playerState = PlayerState.JUMP;
            
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
        velocity.y = MaxJumpVelocity;
        reachedApex = false;
        maxHeightReached = Mathf.Max(transform.position.y, maxHeightReached);
    }
}
