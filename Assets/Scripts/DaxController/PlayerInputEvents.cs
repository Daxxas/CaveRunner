using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (PlayerController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputEvents : MonoBehaviour
{
    private Vector2 input;

    public Vector2 Input
    {
        get => input;
        set => input = value;
    }

    private bool jumpPress;
    private bool jumpRelease;

    public bool JumpPress
    {
        get => jumpPress;
        set => jumpPress = value;
    }

    public bool JumpRelease
    {
        get => jumpRelease;
        set => jumpRelease = value;
    }

    private bool attack;

    public bool Attack
    {
        get => attack;
        set => attack = value;
    }

    private bool action;
    
    public bool Action
    {
        get => action;
        set => action = value;
    }
    
    private void LateUpdate()
    {
        InputReset();
    }
    
    public void InputReset()
    {
        jumpPress = false;
        jumpRelease = false;
        attack = false;
        action = false;
    }
    
    
    void OnMovement(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }

    void OnJumpPress(InputValue inputValue)
    {
        jumpPress = true;
    }
    
    void OnJumpRelease(InputValue inputValue)
    {
        jumpRelease = true;
    }

    void OnAttack(InputValue inputValue)
    {
        attack = true;
    }

    void OnAction(InputValue inputValue)
    {
        action = true;
    }
}
