using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFallingPlatformsLevel : MonoBehaviour
{
    GameObject[] platformsToStartMoving = default;

    // Start is called before the first frame update
    void Start()
    {
        platformsToStartMoving = GameObject.FindGameObjectsWithTag("MovingPlatform");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
         {
            FindObjectOfType<PlatformSpawner>().spawning = true;
            foreach (GameObject platform in platformsToStartMoving)
            {
                platform.GetComponent<PlatformMovement>().SetFallingPlatformSpeed(-2f);
            }      
            Destroy(gameObject);       
         }
        
    }

}
