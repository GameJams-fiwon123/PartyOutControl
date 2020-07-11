using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Kid : Person
{
    public enum states { IDLE, RUN, JUMP, HIDE };

    [Header("More Config")]
    public GameObject detectCollider;

    Vector3 dir = Vector3.zero;
    Vector3 vel = Vector3.zero;

    public states currentState;

    float countTime = 0;
    bool isRun = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //rb2D.bodyType = RigidbodyType2D.Dynamic;
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
        }
        else
        {
            //rb2D.bodyType = RigidbodyType2D.Kinematic;
            vel = Vector3.zero;
            rb2D.velocity = vel;
        }
    }

    private void Hide()
    {
        // Idle
    }

    private void Jump()
    {
        // Run
    }

    private void Run()
    {
        countTime -= Time.deltaTime;

        if (countTime <= 0)
        {
            isRun = true;

            if (Random.Range(0, 100000) > 90000)
            {
                currentState = states.IDLE;
                countTime = Random.Range(1f, 2f);
                return;
            }

            if (Random.Range(0, 10000) > 5000)
            {
                dir = new Vector2(1, 0);
            }
            else
            {
                dir = new Vector2(-1, 0);
            }

            countTime = Random.Range(1f, 5f);
        }

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        vel.x = dir.x * speed * Time.deltaTime;
        vel.y = rb2D.velocity.y;

        rb2D.velocity = vel;
    }

    private void Idle()
    {
        countTime -= Time.deltaTime;

        //Run or Hide or Interact
        if (Random.Range(0, 100000) > 20000 && countTime <= 0)
        {
            currentState = states.RUN;
        }

        vel.x = Vector3.zero.x;
        vel.y = rb2D.velocity.y;

        rb2D.velocity = vel;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Teleport")
        {
            if (Random.Range(0, 100000) > 20000 && isRun)
            {
                isRun = false;
                other.GetComponent<Teleport>().ChangePlace(this);
            }
        }
    }
}
