using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Button toStage2;
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    void Start()
    {
        toStage2 = GetComponent<Button>();
        toStage2 = GameObject.Find("FirstNextButton").GetComponent<Button>();
        toStage2.onClick.AddListener(OnClickToStage2);
    }

    void OnClickToStage2()
    {
        //Debug.Log("You have clicked the button!yeahhh");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
