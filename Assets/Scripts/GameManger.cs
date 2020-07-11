using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public Text textTime;
    public float min = 5;
    public float sec = 0;

    [SerializeField]
    bool gameStarted = false;

    public static GameManger instance;

    private void Start()
    {
        gameStarted = true;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            sec -= Time.deltaTime;
            if (sec < 0)
            {
                sec = 59;
                min -= 1;

                if (min < 0)
                {
                    GameOver();
                }
            }
            textTime.text = string.Format("{0:0}:{1:00}", min, sec);
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        gameStarted = false;
    }
}
