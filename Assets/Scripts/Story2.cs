using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story2 : MonoBehaviour
{
    public void ChangeSceneTutorial(){
        LevelManager.instance.LoadTutorial();
    }
}
