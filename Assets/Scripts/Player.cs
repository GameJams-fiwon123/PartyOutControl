using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    SpriteRenderer sprRenderer;
    Vector3 dir = Vector3.zero;
    Vector3 vel = Vector3.zero;

    public float speed = 250f;
    public float jumpForce = 250f;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        vel = Vector3.zero;
        if (canMove)
        {
            Move();
            Jump();
            InputStair();
        }
        rb2D.velocity = vel;
    }

    private void InputStair()
    {
        if (Input.GetKeyDown(KeyCode.Z) && objTeleport)
            objTeleport.GetComponent<Teleport>().ChangePlace(this);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGround())
        {
            vel.y = jumpForce * Time.deltaTime;
        }
    }

    private bool IsGround()
    {

        LayerMask mask = LayerMask.GetMask("Floor");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 2f, mask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.tag == "Floor")
                return true;
        }

        return false;

    }

    private void Move()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        // dir.y = Input.GetAxisRaw("Vertical");

        if (dir.x > 0)
        {
            sprRenderer.flipX = false;
        }
        else if (dir.x < 0)
        {
            sprRenderer.flipX = true;
        }
        vel.x = dir.x * speed * Time.deltaTime;
        vel.y = rb2D.velocity.y;

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
            objTeleport = other.GetComponent<Teleport>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Teleport")
        {
            objTeleport = null;
        }
    }
}
