using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployDragons : MonoBehaviour
{
    public GameObject dragonPrefab;
    private float rightBorder = -35;
    public Vector2 screenBounds;

    float _upperScreenBound;//4.6f;
    float _lowerScreenBound;//-2.6f;

    // Start is called before the first frame update
    void Start()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        _upperScreenBound = RedBird._upperScreenBound;
        _lowerScreenBound = RedBird._lowerScreenBound;

        StartCoroutine(dragonWave());
    }

    private void spawnEnemy() {

        GameObject a = Instantiate(dragonPrefab) as GameObject;
        a.transform.position = new Vector2(rightBorder+10, Random.Range(_lowerScreenBound+0.5f, _upperScreenBound));
    }

   IEnumerator dragonWave()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(1.0f,2.0f));
            spawnEnemy();
        }
    }
}
