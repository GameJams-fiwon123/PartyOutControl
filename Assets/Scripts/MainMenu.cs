using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Animator anim;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void LoadStory(){
        anim.Play("FadeIn");
    }

    public void ChangeSceneStory1(){
        LevelManager.instance.LoadStory1();
    }
}
