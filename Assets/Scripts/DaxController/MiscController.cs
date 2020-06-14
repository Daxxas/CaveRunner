using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscController : MonoBehaviour
{
    private Controller2D controller;
    [SerializeField] private float gravity = 1f;

    private Vector2 externalForce = Vector2.zero;
    Vector2 velocity;
    void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.collisions.below)
        {
            externalForce.y -= gravity;
            velocity = externalForce * Time.deltaTime;
            externalForce.y -= Time.deltaTime;
        }
        else
        {
            externalForce = Vector2.zero;
            velocity.y = 0;
            velocity.x = 0;
        }
        controller.Move(velocity);
    }
    
    
    public void ExternalMove(Vector2 vector)
    {
        externalForce = vector;
    }
}
