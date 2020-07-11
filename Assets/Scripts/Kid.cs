using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Person
{
    public enum states { IDLE, RUN, JUMP, HIDE };

    Vector3 dir = Vector3.zero;
    Vector3 vel = Vector3.zero;

    states currentState;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentState = states.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            switch (currentState)
            {
                case states.IDLE:
                    Idle();
                    break;
                case states.RUN:
                    Run();
                    break;
                case states.JUMP:
                    Jump();
                    break;
                case states.HIDE:
                    Hide();
                    break;
            }
        }else{
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            vel = Vector3.zero;
            rb2D.velocity = vel;
        }
    }

    private void Hide()
    {
    }

    private void Jump()
    {
    }

    private void Run()
    {
    }

    private void Idle()
    {

    }
}
