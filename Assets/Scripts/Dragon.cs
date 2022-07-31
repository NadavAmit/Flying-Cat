using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private LevelController_A levelController;
    private float speed;
    private Rigidbody2D rb;
    private float leftBorder;
    public float dragonSpeed;
    
    private float last_height_change;
    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);

        var dist = (transform.position - Camera.main.transform.position).z;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

        GameObject ground = GameObject.FindGameObjectWithTag("Background");
        levelController = GameObject.Find("LevelController").GetComponent<LevelController_A>();
        Physics2D.IgnoreCollision(ground.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        last_height_change = Time.time;
       // Physics2D.IgnoreLayerCollision(0, 1);
       // Physics2D.IgnoreLayerCollision(0, 2);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = updatedSpeedAndHeight();
        if (transform.position.x < leftBorder) {
            Destroy(this.gameObject);
        }
    }

    private Vector2 updatedSpeedAndHeight()
    {
        speed = levelController.backgroundSpeed + dragonSpeed;
        float height = 0;
        //// I tried to make the dragon change it's height every two seconds, but it didn't work...
        //Debug.Log("time" + Time.time);
        //Debug.Log("diff: "+ (Time.time - last_height_change));

        //if (Time.time - last_height_change >= 2)
        //{
        //    if (up)
        //        height = 40;
        //    else height = -40;
        //    up = !up;
        //    Debug.Log("any change? ------------------------------------------------ " + height);
        //    last_height_change = Time.time;
        //}
        return new Vector2(-speed, height);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RedBird player = collision.gameObject.GetComponent<RedBird>();  // if it was not hit by the RedBird, then it will be null
        if (player != null)
        {
            Debug.Log("A dragon just hit the cat!");
            levelController.GameOver();
        }
    }


    bool ShouldDieFromCollision(Collision2D collision)
    {
        RedBird bird = collision.gameObject.GetComponent<RedBird>();
        if (bird != null)
        { return true; }

        return false;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0);
        Destroy(this.gameObject);
        //gameObject.SetActive(false);
    }
}
