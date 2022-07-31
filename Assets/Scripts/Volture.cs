using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volture : MonoBehaviour
{
    [SerializeField] GameObject flame;
    [SerializeField] Transform flamePosition;
    [SerializeField] public AudioSource wingFlap;
    [SerializeField] public AudioSource fireSpit;

    GameObject bird;
    Rigidbody2D rb;
    private LevelController_A levelController;

    float shootRate = 2.0f;
    float _upperScreenBound = 6f;
    float _lowerScreenBound = -0.5f;
    float _flySpeed = 2.0f;
    bool spitting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        bird = GameObject.Find("Red Bird");
        GameObject ground = GameObject.FindGameObjectWithTag("Background");
        levelController = GameObject.Find("LevelController").GetComponent<LevelController_A>();
        //this.gameObject.transform.Translate(Vector2.up * _flySpeed * Time.deltaTime, Space.World);
        Physics2D.IgnoreCollision(ground.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
    }

    void Hover()
    {
        if (!spitting)
        {
            wingFlap.Play();
            rb.velocity = new Vector2(0, _flySpeed);
            StartCoroutine(SpitFlames());
            spitting = true;
        }
        if (gameObject.transform.position.y >= _upperScreenBound)
        {
            rb.velocity = new Vector2(0, -1 * _flySpeed);
        }
        else if (gameObject.transform.position.y <= _lowerScreenBound)
        {
            rb.velocity = new Vector2(0, _flySpeed);
        }
        //gameObject.transform.position = bird.transform.position + new Vector3(14f,0f);
    }
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(gameObject.transform.position.x - bird.transform.position.x) > 14f)
        {
            rb.velocity = new Vector2(-2*_flySpeed, 0f);
        }
        else
        {
            Hover();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    void SpitFlame()
    {
        Vector2 directionToTarget = bird.transform.position - transform.position;
        float angle = Vector2.Angle(Vector2.left, directionToTarget);
        if (bird.transform.position.y > this.gameObject.transform.position.y) angle *= -1; 
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(this.flame, flamePosition.position, bulletRotation);
        fireSpit.Play();
    }

    IEnumerator SpitFlames()
    {
        RedBird b = bird.GetComponent<RedBird>();
        while (b.stillAlive)
        {
            yield return new WaitForSeconds(shootRate);
            SpitFlame();
        }
    }
}
