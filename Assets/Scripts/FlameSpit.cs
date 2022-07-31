using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpit : MonoBehaviour
{
    LevelController_A levelController;
    GameObject bird;
    float flameSpeed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.Find("Red Bird");
        GameObject ground = GameObject.FindGameObjectWithTag("Background");
        levelController = GameObject.Find("LevelController").GetComponent<LevelController_A>();
        Physics2D.IgnoreCollision(ground.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if(bird != null)
            this.GetComponent<Rigidbody2D>().velocity = (bird.transform.position - transform.position).normalized * flameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RedBird player = collision.gameObject.GetComponent<RedBird>();  // if it was not hit by the RedBird, then it will be null
        if (player != null)
        {
            Debug.Log("Flame hit cat");
            if (!player.IsShielded())
            {
                levelController.GameOver();
            }
            gameObject.SetActive(false);
        }
    }
}
