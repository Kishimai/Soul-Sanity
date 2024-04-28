using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameManager gameManager;

    public void Play(){
        SceneManager.LoadScene("Main");
    }

    public void Settings(){
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Back2Menu(){
        SceneManager.LoadScene("Menu");
    }
}
