using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoxChanger : MonoBehaviour
{
    
    public float speedMultiplier = 1f;
    public SurfaceEffector2D surfaceEffector;

    float multiplier;
    float startTime;
    float elapsedTime;
    float theTime;

    void Start()
    {
        startTime = Time.time;
        theTime = 120f;
    }

    // Update is called once per frame
    void Update()
    {   
        elapsedTime = Time.time - startTime;
        theTime -= Time.deltaTime;

        if(theTime > 0){
            multiplier = Mathf.Lerp(90, 500, elapsedTime * speedMultiplier);
        } else {
            theTime = 0;
            multiplier *= 0.998f;
            if(multiplier < 60){
                multiplier = 0;
            }
        }

        surfaceEffector.speed = -1 * multiplier * Time.fixedDeltaTime;
        
    }
}
