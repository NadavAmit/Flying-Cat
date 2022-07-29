using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController_A : MonoBehaviour
{
    [SerializeField] string _nextLevelName;
    [SerializeField] public float backgroundSpeed;
    [SerializeField] public static bool godMode = false;
    [SerializeField] public int[] coinsForNextLevel = {25, 50, 100};

    Scene scene;
    public GameOverScreen gameOverScreen;
    RedBird player;
    private int totalCoins;
    Dragon[] _dragons;

    void OnEnable()
    {
        _dragons = FindObjectsOfType<Dragon>();
        player = GameObject.Find("Red Bird").GetComponent<RedBird>();
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.falling[2] == false)  // player died and finished fall
            gameOverScreen.Setup(Prize.totalCoins);
        else
        {
            totalCoins = Prize.totalCoins;
            if (totalCoins >= coinsForNextLevel[0]  &&  scene.name == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (totalCoins >= coinsForNextLevel[1] && scene.name == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }
            else if (totalCoins >= coinsForNextLevel[2] && scene.name == "Level3")
            {
                // finish the game!
            }
        }
    }



    private void GoToNextLevel()
    {
        Debug.Log("Go To Level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    private bool MonstersAreAllDead()
    {
        foreach (var dragon in _dragons) {
            if (dragon.gameObject.activeSelf)
                return false;
        }

        return true;
    }

    public void GameOver()
    {
        if (!godMode)
        {
            player.Die();
            // wait until cat falls!
        }
        else
        {
            Debug.Log("Can't die when in god mod");
        }

    }
}
