using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject[] platform;
    public float randX; // x koordinata
    Vector2 whereToSpawn;
    public float SpawnRate = 2f; //znači na 2 sekunde
    float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFallingPlatforms();
    }

    private void SpawnFallingPlatforms()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + SpawnRate;
            randX = Random.Range(-12f, 6.3f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            var platformToBeSpawned = platform[Random.Range(0, platform.Length)];
            if (platformToBeSpawned.CompareTag("FallingMovingPlatform"))
            {
                Instantiate(platformToBeSpawned, transform.position, Quaternion.identity);
            }
            else
            {    
                Instantiate(platformToBeSpawned, whereToSpawn, Quaternion.identity);
            } 
        }
    }
}
