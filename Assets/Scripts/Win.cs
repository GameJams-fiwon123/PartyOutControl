using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    public void ChangeSceneCredits()
    {
        AudioManager.instance.ChangeToMainMenu();
        LevelManager.instance.LoadCredits();
    }

    public void ChangeSceneMainMenu()
    {
        AudioManager.instance.ChangeToMainMenu();
        LevelManager.instance.LoadMainMenu();
    }
}
