using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform toPlace;
    public bool isUp;

    public void ChangePlace(Person p)
    {
        StartCoroutine("ProcessChange", p);
    }

    IEnumerator ProcessChange(Person p)
    {
        p.canMove = false;
        p.boxCol.enabled = false;
        while (Vector3.Distance(gameObject.transform.position, p.transform.position) > 0.5f)
        {
            p.anim.Play("Run");
            Vector3 dir = gameObject.transform.position - p.transform.position;
            if (dir.x > 0)
            {
                p.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                p.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            p.rb2D.velocity = dir.normalized * p.speed * Time.deltaTime;
            yield return null;
        }

        while (Vector3.Distance(toPlace.position, p.transform.position) > 0.5f)
        {
            p.anim.Play("Run");
            Vector3 dir = toPlace.position - p.transform.position;
            if (dir.x > 0)
            {
                p.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                p.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            p.rb2D.velocity = dir.normalized * p.speed * Time.deltaTime;
            yield return null;
        }

        p.rb2D.velocity = Vector2.zero;
        p.boxCol.enabled = true;
        p.canMove = true;
        p.isStair = false;
    }
}
