﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverSceneSwitch()
    {
        if(GameObject.FindObjectOfType<DialogueGlobals>() != null)
            GameObject.FindObjectOfType<DialogueGlobals>().ResetRemainingWC();
        if (GameObject.FindObjectOfType<GameController>() != null)
            GameObject.FindObjectOfType<GameController>().ResetVars();

        SceneManager.LoadScene("Day1");
    }
}
