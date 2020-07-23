using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story1 : MonoBehaviour
{
    public void ChangeSceneStory2(){
        LevelManager.instance.LoadStory2();
    }
}
