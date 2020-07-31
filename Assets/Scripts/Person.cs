using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    
    [Header("Configs")]
    public Rigidbody2D rb2D;
    public BoxCollider2D boxCol;
    public Animator anim;

    [Header("Properties")]
    public float speed = 250f;
    public float jumpForce = 250f;
    public bool canMove = true;
    public bool isStair = false;

    [HideInInspector]
    public Teleport objTeleport = null;

}
