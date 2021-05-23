using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    
    public Animator animator;
    public float conveyorSpeed = 1f;
    public SurfaceEffector2D surfaceEffector;
    AudioSource audioSrc;

    float theTime;
    float audioPitch = 1.00008f;
    float pushMultiplier;
    float startTime;
    float elapsedTime;

    void Start()
    {
        startTime = Time.time;
        audioSrc = GetComponent<AudioSource>();
        theTime = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        theTime -= Time.deltaTime;

        if(theTime > 0){
        pushMultiplier = Mathf.Lerp(90, 500, elapsedTime * conveyorSpeed);
        audioSrc.volume = 0.51f;

        audioSrc.pitch = audioSrc.pitch * audioPitch;
        if(audioSrc.pitch > 2.6f){
            audioSrc.pitch = 2.6f;
        }
        } else {
            theTime = 0;
            pushMultiplier *= 0.998f;
            if(pushMultiplier < 60f){
                pushMultiplier = 0;
            }
            audioSrc.pitch *= 0.999f;
            audioSrc.volume *= 0.996f;
        if(audioSrc.pitch < 0.6f){
            audioSrc.Stop();
        }
        }
        animator.SetFloat("SpeedMultiplier", pushMultiplier * Time.fixedDeltaTime);
        surfaceEffector.speed = -7 * pushMultiplier * Time.fixedDeltaTime;
        
    }
}
