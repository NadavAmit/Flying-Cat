using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {

        SceneManager.LoadScene("Level1.0");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame() {

        Debug.Log("QUIT!");
        Application.Quit();
    }
}
