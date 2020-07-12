using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour
{
    public Kid kid;

    public void Hide(Kid k){
        kid = k;
        kid.transform.position = gameObject.transform.position;
    }
}
