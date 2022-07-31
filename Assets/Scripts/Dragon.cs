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
    
    private float upperLimit;
    private float lowerLimit;
    enum State { UP, DOWN };
    State state = State.UP;

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
        upperLimit = Mathf.Min(gameObject.transform.position.y + 2f, RedBird._upperScreenBound);
        lowerLimit = Mathf.Max(gameObject.transform.position.y - 2f, RedBird._lowerScreenBound);
        
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
        if(state == State.UP)
        {
            if (gameObject.transform.position.y <= upperLimit)
            {
                return new Vector2(-speed, speed/2);
            }
            else
            {
                state = State.DOWN;
                return new Vector2(-speed, -speed/2);
            }
        }
        else
        {
            if(gameObject.transform.position.y >= lowerLimit)
            {
                return new Vector2(-speed, -speed/2);
            }
            else
            {
                state = State.UP;
                return new Vector2(-speed, speed/2);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Dragon"))
        {
            RedBird player = collision.gameObject.GetComponent<RedBird>();  // if it was not hit by the RedBird, then it will be null
            if (player != null)
            {
                Debug.Log("A dragon just hit the cat!");
                if (!player.IsShielded())
                {
                    levelController.GameOver();
                }
                gameObject.SetActive(false);
            }
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
