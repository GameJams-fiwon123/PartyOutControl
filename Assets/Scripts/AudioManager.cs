using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioMusic;

    public AudioClip musicGame;
    public AudioClip musicMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeToGame(){
        audioMusic.clip = musicGame;
        audioMusic.Play();
    }

    public void ChangeToMainMenu(){
        audioMusic.clip = musicMainMenu;
        audioMusic.Play();
    }

    public void StopMusic(){
        audioMusic.Stop();
    }
}
