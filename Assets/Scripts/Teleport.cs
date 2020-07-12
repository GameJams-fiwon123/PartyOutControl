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
        p.anim.Play("Run");
        while (Vector3.Distance(gameObject.transform.position, p.transform.position) > 0.1f)
        {
            Vector3 dir = gameObject.transform.position - p.transform.position;
            if (dir.x > 0)
            {
                p.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                p.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            p.transform.position += dir.normalized * 2.5f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }

        while (Vector3.Distance(toPlace.position, p.transform.position) > 0.1f)
        {
            Vector3 dir = toPlace.position - p.transform.position;
            if (dir.x > 0)
            {
                p.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                p.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            p.transform.position += dir.normalized * 2.5f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        p.boxCol.enabled = true;
        p.canMove = true;
    }
}
