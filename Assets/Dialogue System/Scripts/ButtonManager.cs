using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
   
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Introduction Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Day1Scene()
    {
        SceneManager.LoadSceneAsync("Day1");
    }
}
