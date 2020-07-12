using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ChangeSceneMainMenu()
    {
        AudioManager.instance.ChangeToMainMenu();
        LevelManager.instance.LoadMainMenu();
    }
}
