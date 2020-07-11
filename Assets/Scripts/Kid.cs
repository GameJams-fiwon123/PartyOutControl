using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Person
{  
    public enum states {IDLE, RUN, JUMP, HIDE};

    states currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState){
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
