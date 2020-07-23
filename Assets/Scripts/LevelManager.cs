using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        instance = this;
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void LoadStory1(){
        SceneManager.LoadScene("Story1");
    }

    public void LoadStory2(){
        SceneManager.LoadScene("Story2");
    }

    public void LoadGameOver(){
        SceneManager.LoadScene("GameOver");
    }

    public void LoadWinGame(){
        SceneManager.LoadScene("WinGame");
    }

    public void LoadCredits(){
        SceneManager.LoadScene("Credits");
    }

    public void LoadTutorial(){
        SceneManager.LoadScene("Tutorial");
    }
}
