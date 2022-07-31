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
    [SerializeField] public AudioSource deathSong;
    [SerializeField] public AudioSource catDeath;

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
                GameObject.Find("ThemeSong1").GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Level2");
            }
            else if (totalCoins >= coinsForNextLevel[1] && scene.name == "Level2")
            {
                GameObject.Find("ThemeSong2").GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Level3");
            }
            else if (totalCoins >= coinsForNextLevel[2] && scene.name == "Level3")
            {
                GameObject.Find("ThemeSong3").GetComponent<AudioSource>().Stop();
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
            if(scene.name == "Level1")
            {
                GameObject.Find("ThemeSong1").GetComponent<AudioSource>().Stop();
                GameObject.Find("BirdsFlying").GetComponent<AudioSource>().Stop();
            }
            else if (scene.name == "Level2")
            {
                GameObject.Find("ThemeSong2").GetComponent<AudioSource>().Stop();
                GameObject.Find("DragonGrowl").GetComponent<AudioSource>().Stop();
            }
            else if (scene.name == "Level3")
            {
                GameObject.Find("ThemeSong3").GetComponent<AudioSource>().Stop();
                GameObject.Find("WingFlap").GetComponent<AudioSource>().Stop();
                GameObject.Find("Bats").GetComponent<AudioSource>().Stop();
            }
            deathSong.Play();
            catDeath.Play();
            player.Die();
            // wait until cat falls!
        }
        else
        {
            Debug.Log("Can't die when in god mod");
        }

    }
}
