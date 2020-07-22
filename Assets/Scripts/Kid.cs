using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Kid : Person
{
    public enum states { IDLE, RUN, JUMP, HIDE };

    [Header("More Config")]
    public BoxCollider2D detectCollider;
    public SpriteRenderer sprRenderer;

    public AudioSource audioLaugh;

    Vector3 dir = Vector3.zero;
    Vector3 vel = Vector3.zero;

    public states currentState;

    public float countTime = 0;
    bool isRun = false;
    float timeLaugh;

    float timeRunPlayer = 0;

    bool hidePlayerSee = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.IDLE;
        timeLaugh = Random.Range(1f, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            PlayLaugh();

            detectCollider.enabled = true;
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
            audioLaugh.Stop();
            detectCollider.enabled = false;
            vel = Vector3.zero;
            rb2D.velocity = vel;
        }
    }

    private void PlayLaugh()
    {
        if (!audioLaugh.isPlaying)
            timeLaugh -= Time.deltaTime;

        if (timeLaugh <= 0 && !audioLaugh.isPlaying)
        {
            audioLaugh.Play();
            timeLaugh = Random.Range(1f, 5f);
        }
    }

    private void Hide()
    {
        countTime -= Time.deltaTime;

        if (Random.Range(0, 100000) > 95000 && countTime < 0)
        {
            if ((!SeePlayerBackward(2.5f) && !SeePlayerForward(2.5f)) || hidePlayerSee)
            {
                countTime = Random.Range(0f, 1.5f);
                sprRenderer.enabled = true;
                currentState = states.IDLE;
            }
        }
        vel.x = Vector3.zero.x;
        vel.y = rb2D.velocity.y;
        rb2D.velocity = vel;
    }

    private void Jump()
    {
        if (IsGround())
        {
            vel.y = jumpForce * Time.deltaTime;
        }

        rb2D.velocity = vel;
        currentState = states.RUN;
    }

    private bool IsGround()
    {

        LayerMask mask = LayerMask.GetMask("Floor");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, mask);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Floor")
                return true;
        }

        return false;

    }

    private void Run()
    {
        countTime -= Time.deltaTime;
        anim.Play("Run");

        if (Random.Range(0, 100000) > 90000)
        {
            currentState = states.JUMP;
        }

        if (countTime <= 0 || IsWallClose() || (SeePlayerForward(3.5f) && !IsWallCloseBackward(5f) && countTime > 0.1f))
        {
            isRun = true;

            if (Random.Range(0, 100000) > 90000)
            {
                currentState = states.IDLE;
                countTime = Random.Range(0f, 1.5f);
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

    private bool SeePlayerForward(float distance)
    {
        LayerMask mask = LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, mask);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

    private bool SeePlayerBackward(float distance)
    {
        LayerMask mask = LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -dir, distance, mask);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

    private bool IsWallClose()
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.5f, mask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.tag == "Wall")
                return true;
        }

        return false;
    }

    private bool IsWallCloseBackward(float distance)
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -dir, distance, mask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.tag == "Wall")
                return true;
        }

        return false;
    }

    private void Idle()
    {
        countTime -= Time.deltaTime;
        anim.Play("Idle");

        //Run or Hide or Interact
        if ((Random.Range(0, 100000) > 20000 && countTime <= 0) || SeePlayerForward(3.5f))
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
        if (canMove)
        {
            if (other.tag == "Teleport")
            {
                if (Random.Range(0, 100000) > 50000 && isRun)
                {
                    isRun = false;
                    detectCollider.enabled = false;
                    other.GetComponent<Teleport>().ChangePlace(this);
                }
            }
            else if (other.tag == "HidePlace")
            {
                int offset = 0;
                if (!SeePlayerBackward(8f) && !SeePlayerForward(8f))
                {
                    offset = 25000;
                }

                if (Random.Range(0, 100000) > (90000 - offset) && isRun && !other.GetComponent<HidePlace>().kid)
                {
                    if (SeePlayerBackward(3.5f) && SeePlayerForward(3.5f))
                    {
                        hidePlayerSee = true;
                    }

                    isRun = false;
                    currentState = states.HIDE;
                    countTime = Random.Range(10f, 15f);
                    sprRenderer.enabled = false;
                    other.GetComponent<HidePlace>().Hide(this);
                }
            }
        }
    }
}
