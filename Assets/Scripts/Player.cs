using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{

    Vector3 dir = Vector3.zero;
    Vector3 vel = Vector3.zero;

    GameObject kidInteract = null;
    GameObject holdKid = null;

    [Header("More Configs")]
    public Transform posHoldKid;
    public Animator handAnim;
    public Transform kidsTransform;


    // Update is called once per frame
    void Update()
    {
        vel = Vector3.zero;
        if (canMove)
        {
            Move();
            Jump();
            InputStair();
            InputInteract();
        }
        rb2D.velocity = vel;
    }

    private void InputInteract()
    {
        if (Input.GetKeyDown(KeyCode.X) && kidInteract && !holdKid)
        {
            handAnim.Play("HoldHand");
            kidInteract.transform.localScale = gameObject.transform.localScale;
            kidInteract.transform.position = posHoldKid.position;
            kidInteract.transform.parent = posHoldKid.transform;
            kidInteract.GetComponent<Kid>().canMove = false;
            kidInteract.GetComponent<Kid>().rb2D.bodyType = RigidbodyType2D.Kinematic;
            holdKid = kidInteract;
            holdKid.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            kidInteract = null;
        }
        else if (Input.GetKeyDown(KeyCode.X) && holdKid)
        {
            handAnim.Play("NoneHand");
            holdKid.GetComponent<Kid>().canMove = true;
            holdKid.GetComponent<Kid>().rb2D.bodyType = RigidbodyType2D.Dynamic;
            holdKid.transform.parent = kidsTransform;
            holdKid.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            holdKid = null;
        }
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, mask);

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
            transform.localScale = new Vector3(1, 1, 1);
            anim.Play("Run");
            if(!holdKid)
                handAnim.Play("NoneHand");
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.Play("Run");
            if(!holdKid)
                handAnim.Play("NoneHand");
        } else {
            anim.Play("Idle");
            if(!holdKid)
                handAnim.Play("IdleNoneHand");
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
        else if (other.tag == "Kid")
        {
            kidInteract = other.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Teleport")
        {
            objTeleport = null;
        }
        else if (other.tag == "Kid" && kidInteract && other.gameObject.Equals(kidInteract.GetComponent<Kid>().detectCollider))
        {
            kidInteract = null;
        }
    }
}
