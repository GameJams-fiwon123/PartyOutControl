using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public bool canMove = true;
    public Rigidbody2D rb2D;
    public BoxCollider2D boxCol;

    public Teleport objTeleport = null;

}
