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
    GameObject objectivePlaceObj;
    HidePlace hidePlace;

    private bool isStair = false;


    // Update is called once per frame
    void Update()
    {
        vel = Vector3.zero;
        if (canMove && GameManger.instance.gameStarted)
        {
            Move();
            // Jump();
            InputStair();
            InputInteract();
        }
    }

    void FixedUpdate()
    {
        if (canMove && GameManger.instance.gameStarted)
        {
            vel.x = dir.x * speed * Time.deltaTime;
            vel.y = rb2D.velocity.y;

            rb2D.velocity = vel;

            if (isStair){
                objTeleport.GetComponent<Teleport>().ChangePlace(this);
                isStair = false;

            }

        } else {
            rb2D.velocity = Vector2.zero;
        }
    }

    private void InputInteract()
    {
        if (Input.GetKeyDown(KeyCode.Space) && holdKid && objectivePlaceObj) // Objective
        {
            holdKid.GetComponent<Kid>().sprRenderer.sortingOrder = 3;
            GameManger.instance.PutKid(holdKid);
            holdKid = null;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && kidInteract && !holdKid  // Hold kid
                && kidInteract.GetComponent<Kid>().currentState != Kid.states.HIDE
                && kidInteract.GetComponent<Kid>().canMove
                && Vector2.Distance(kidInteract.transform.position, gameObject.transform.position) < 1)
        {
            handAnim.Play("HoldHand");
            kidInteract.transform.localScale = gameObject.transform.localScale;
            kidInteract.transform.position = posHoldKid.position;
            kidInteract.transform.parent = posHoldKid.transform;
            kidInteract.GetComponent<Kid>().canMove = false;
            kidInteract.GetComponent<Kid>().rb2D.bodyType = RigidbodyType2D.Kinematic;
            kidInteract.GetComponent<Kid>().sprRenderer.sortingOrder = 1;
            kidInteract.GetComponent<Kid>().detectCollider.enabled = false;
            kidInteract.GetComponent<Kid>().anim.Play("Idle");
            holdKid = kidInteract;
            holdKid.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            kidInteract = null;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && holdKid && !objectivePlaceObj) // Drop Kid
        {
            handAnim.Play("NoneHand");
            holdKid.GetComponent<Kid>().canMove = true;
            holdKid.GetComponent<Kid>().rb2D.bodyType = RigidbodyType2D.Dynamic;
            holdKid.GetComponent<Kid>().sprRenderer.sortingOrder = 3;
            holdKid.GetComponent<Kid>().detectCollider.enabled = true;
            holdKid.transform.parent = kidsTransform;
            holdKid.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            holdKid = null;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && hidePlace && !holdKid)  // Find Kid
        {
            if (hidePlace.kid)
            {
                hidePlace.kid.sprRenderer.enabled = true;
                hidePlace.kid.currentState = Kid.states.IDLE;
                hidePlace.kid.countTime = UnityEngine.Random.Range(0f, 1.5f);
                hidePlace.kid = null;
            }
        }
    }

    private void InputStair()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) && objTeleport && objTeleport.GetComponent<Teleport>().isUp) ||
            (Input.GetKeyDown(KeyCode.DownArrow) && objTeleport && !objTeleport.GetComponent<Teleport>().isUp))
            isStair = true;

    }

    // private void Jump()
    // {
    //     if (Input.GetButtonDown("Jump") && IsGround())
    //     {
    //         vel.y = jumpForce * Time.deltaTime;
    //     }
    // }

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

    private void Move()
    {
        dir.x = Input.GetAxisRaw("Horizontal");

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.Play("Run");
            if (!holdKid)
                handAnim.Play("NoneHand");
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.Play("Run");
            if (!holdKid)
                handAnim.Play("NoneHand");
        }
        else
        {
            anim.Play("Idle");
            if (!holdKid)
                handAnim.Play("IdleNoneHand");
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "ObjectivePlace")
        {
            objectivePlaceObj = other.gameObject;
        }
        else if (other.tag == "Kid")
        {
            if (other.transform.parent.GetComponent<Kid>().currentState != Kid.states.HIDE)
                kidInteract = other.transform.parent.gameObject;
        }
        else if (other.tag == "HidePlace")
        {
            HidePlace auxHidPlace = other.GetComponent<HidePlace>();
            if (auxHidPlace.kid)
                hidePlace = auxHidPlace;
        }
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
        else if (other.tag == "ObjectivePlace")
        {
            objectivePlaceObj = null;
        }
        else if (other.tag == "HidePlace")
        {
            hidePlace = null;
        }
    }
}
