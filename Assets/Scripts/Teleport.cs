using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform toPlace;

    public void ChangePlace(Person p)
    {
        StartCoroutine("ProcessChange", p);
    }

    IEnumerator ProcessChange(Person p)
    {
        p.canMove = false;
        p.boxCol.enabled = false;
        while (Vector3.Distance(toPlace.position, p.transform.position) > 0.1f)
        {
            Vector3 dir = toPlace.position - p.transform.position;
            p.transform.position += dir.normalized * 2.5f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        p.boxCol.enabled = true;
        p.canMove = true;
    }
}
