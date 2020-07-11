using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void GameOver(){
        SceneManager.LoadScene("GameOver");
    }

    public void LoadWinGame(){
        SceneManager.LoadScene("WinGame");
    }
}
