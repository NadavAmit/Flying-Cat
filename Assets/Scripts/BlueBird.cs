using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    private LevelController_A levelController;
    private float speed;
    private Rigidbody2D rb;
    private float leftBorder;
    public float blueBirdSpeed;

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
        GameObject confiner = GameObject.Find("Confiner");
        if (confiner != null)
            Physics2D.IgnoreCollision(confiner.GetComponent<PolygonCollider2D>(), GetComponent<Collider2D>());
        // Physics2D.IgnoreLayerCollision(0, 1);
        // Physics2D.IgnoreLayerCollision(0, 2);

    }

    // Update is called once per frame
    void Update()
    {
        speed = levelController.backgroundSpeed + blueBirdSpeed;
        rb.velocity = new Vector2(-speed, 0.2f);
        if (transform.position.x < leftBorder) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
