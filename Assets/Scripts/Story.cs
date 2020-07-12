using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public void ChangeSceneGame(){
        AudioManager.instance.ChangeToGame();
        LevelManager.instance.LoadGame();
    }
}
