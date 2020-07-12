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
    public Transform transformKids;
    float countTimeLeave = 10;
    int countKids = 0;

    [SerializeField]
    public bool gameStarted = false;

    public static GameManger instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameStarted = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            TimeCount();
            VerifyKids();
            if (countKids > 0)
            {
                countTimeLeave -= Time.deltaTime;
                if (countTimeLeave <= 0)
                {
                    countTimeLeave = 0;
                    LeaveAnyKid();
                }
            }
        }
    }

    private void LeaveAnyKid()
    {
        int luckNumber = Random.Range(0, 10000);
        if (luckNumber >= 567 - (10 * countKids) && luckNumber <= 567 + (10 * countKids))
        {
            int index = 0;
            do
            {
                index = Random.Range(0, trasformObjective.childCount);
            } while (trasformObjective.GetChild(index).childCount == 0);

            Kid kid = trasformObjective.GetChild(index).GetChild(0).GetComponent<Kid>();

            kid.canMove = true;
            kid.rb2D.bodyType = RigidbodyType2D.Dynamic;
            kid.detectCollider.enabled = true;
            kid.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            kid.gameObject.transform.parent = transformKids;
            countTimeLeave = Random.Range(10, 15 - countKids);
            countKids--;
        }
    }

    private void VerifyKids()
    {
        bool isFinished = true;

        foreach (Transform t in trasformObjective)
        {
            if (t.childCount == 0)
            {
                isFinished = false;
                break;
            }
        }

        if (isFinished)
        {
            LevelManager.instance.LoadWinGame();
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
                min = 0;
                sec = 0;
                GameOver();
            }
        }
        textTime.text = string.Format("{0:0}:{1:00}", min, sec);
    }

    private void GameOver()
    {
        LevelManager.instance.LoadGameOver();
        gameStarted = false;
    }

    public void PutKid(GameObject kid)
    {
        int index = 0;
        do
        {
            index = Random.Range(0, trasformObjective.childCount);
        } while (trasformObjective.GetChild(index).childCount > 0);

        countKids++;
        kid.transform.position = trasformObjective.GetChild(index).position;
        kid.transform.parent = trasformObjective.GetChild(index);

    }
}
