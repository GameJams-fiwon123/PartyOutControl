using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public void ChangeSceneGame()
    {
        AudioManager.instance.ChangeToGame();
        LevelManager.instance.LoadGame();
    }
}
