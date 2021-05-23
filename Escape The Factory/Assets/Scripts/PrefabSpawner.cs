using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab1, prefab2, prefab3, prefab4;
    
    float spawnRate = 1.7f;
    float nextSpawn = 0f;
    int whatToSpawn;
    float theTime;
    float randomizer;
    float theRandom;

    void Start(){
        theTime = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        theTime -= Time.deltaTime;
        if(theTime > 0){
        if(Time.time > nextSpawn)
        {
            whatToSpawn = Random.Range(1,5);

            switch (whatToSpawn)
            {
                case 1: 
                    Instantiate(prefab1, transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(prefab2, transform.position, Quaternion.identity);
                    break;
                case 3: 
                    Instantiate(prefab3, transform.position, Quaternion.identity);
                    break;
                case 4: 
                    Instantiate(prefab4, transform.position, Quaternion.identity);
                    break;
            }

            theRandom = Random.Range(1,7);

            switch(theRandom)
            {
                case 1: 
                    randomizer = -0.2f;
                    break;
                case 2:
                    randomizer = -0.1f;
                    break;
                case 3: 
                    randomizer = 0f;
                    break;
                case 4: 
                    randomizer = 0.1f;
                    break;
                case 5: 
                    randomizer = 0.2f;
                    break;
                case 6: 
                    randomizer = -0.3f;
                    break;
            }
            
            nextSpawn = Time.time + spawnRate + randomizer;
            spawnRate = spawnRate * 0.99f;
            if (spawnRate < 0.99f){
                spawnRate = 0.99f;
            }
        }
        }
    }
}
