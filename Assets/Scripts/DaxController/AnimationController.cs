using System.Collections;
using System.Collections.Generic;
using playerState;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement playerMovement;
    private PlayerInputEvents playerInput;
    private Animator animator;
    private SpriteRenderer sprite;
    
    private bool FacingRight = true;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInputEvents>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        if (playerInput.Input.x > 0 && !FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (playerInput.Input.x < 0 && FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        animator.SetFloat("VelocityX", Mathf.Abs(playerInput.Input.x));
        animator.SetFloat("VelocityY", playerMovement.Velocity.y);
        animator.SetBool("TouchingGround", playerMovement.Controller.collisions.below);
        animator.SetBool("TouchingWall", playerMovement.Controller.collisions.left ||playerMovement.Controller.collisions.right);



    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        sprite.flipX = FacingRight;
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
    }
    
}
