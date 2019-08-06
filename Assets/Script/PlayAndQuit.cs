using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAndQuit : MonoBehaviour
{
    int level;

    public void playGame()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level+1);
    }
     public void QuitGame()
    {
        Application.Quit();
    }
}
