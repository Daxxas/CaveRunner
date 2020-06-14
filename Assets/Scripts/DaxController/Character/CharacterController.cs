using System.Collections;
using System.Collections.Generic;
using characterState;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public abstract class CharacterController : MonoBehaviour
{
    
    public CharacterState characterState;
    protected Controller2D controller;
    protected bool attack;

    public bool canMove = true;
    
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 4f;
    [SerializeField] protected float accelerationTimeAirborne = 0f;
    [SerializeField] protected float accelerationTimeGrounded = 0f;
    [Header("Jump")]
    [SerializeField] protected float MaxjumpHeight = 4f;
    [SerializeField] protected float MinjumpHeight = 1f;
    [SerializeField] protected float timeJumpApex = .4f;
    [SerializeField] protected float gravityFallScale = 1;
    [Header("Walljump")] 
    [SerializeField] protected bool canWallJump = true;
    [SerializeField] protected bool autoWallSlide = true;
    protected bool wallSliding = false;
    protected int wallDirX;
    [SerializeField] protected float wallSlideSpeedMax = 6f;
    [SerializeField] protected float wallStickTime = 0.25f;
    protected float timeToWallUnStick;
    [SerializeField] protected Vector2 wallJumpClimb;
    [SerializeField] protected Vector2 wallJumpOff;
    [SerializeField] protected Vector2 wallJumpLeap;
    public bool Attack
    {
        get => attack;
        set => attack = value;
    }

    public Controller2D Controller
    {
        get => controller;
        set => controller = value;
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    protected Vector3 velocity;

    public virtual void Start()
    {
        controller = GetComponent<Controller2D>();
    }
}
