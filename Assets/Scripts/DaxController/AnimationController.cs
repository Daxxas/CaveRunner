using System.Collections;
using System.Collections.Generic;
using characterState;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;
    private CharacterCombat characterCombat;
    private PlayerInputEvents playerInputs;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool hasCombatComponent = false;
    private bool FacingRight = true;

    public AnimatorStateInfo currentAnimation;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (TryGetComponent<CharacterCombat>(out characterCombat))
        {
            hasCombatComponent = true;
        }
        playerInputs = GetComponent<PlayerInputEvents>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (characterController.Velocity.x > 0 && !FacingRight)
        {
            Flip();
        }
        else if (characterController.Velocity.x < 0 && FacingRight)
        {
            Flip();
        }

        if (hasCombatComponent)
        {
            if (characterCombat.engageAttack)
            {
                animator.SetTrigger("Attacking");
                characterCombat.engageAttack = false;
            }
            
            if (characterCombat.gotHit)
            {
                animator.SetTrigger("Hitten");
                characterCombat.gotHit = false;
            }

            if (characterCombat.isDead)
            {
                animator.SetTrigger("Dead");
                characterCombat.isDead = false;
            }
        }
        
        animator.SetFloat("VelocityX", Mathf.Abs(characterController.Velocity.x));
        animator.SetFloat("VelocityY", characterController.Velocity.y);
        animator.SetBool("TouchingGround", characterController.Controller.collisions.below);
        animator.SetBool("TouchingWall", characterController.Controller.collisions.left || characterController.Controller.collisions.right);
        
        currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

    }

    private void Flip()
    {
        sprite.flipX = FacingRight;
        FacingRight = !FacingRight;
    }
    
}
