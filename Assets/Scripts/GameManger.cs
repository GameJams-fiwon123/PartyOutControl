using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour
{
    public Text textTime;
    public float min = 5;
    public float sec = 0;

    public Transform trasformObjective;

    [SerializeField]
    public bool gameStarted = false;

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
            TimeCount();
            VerifyKids();
        }
    }

    private void VerifyKids()
    {
        bool isFinished = true;

        foreach(Transform t in trasformObjective){
            if (t.childCount == 0){
                isFinished = false;
                break;
            }
        }

        if (isFinished){
            Debug.Log("You Win");
            gameStarted = false;
        }
    }

    private void TimeCount()
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

    private void GameOver()
    {
        Debug.Log("GameOver");
        gameStarted = false;
    }

    public void PutKid(GameObject kid)
    {
        int index = 0;
        do
        {
            index = Random.Range(0, trasformObjective.childCount);
        } while (trasformObjective.GetChild(index).childCount > 0);

        kid.transform.position = trasformObjective.GetChild(index).position;
        kid.transform.parent = trasformObjective.GetChild(index);

    }
}
