using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    float theTime;
    AudioSource opensfx;
    public AudioClip Open;

    // Start is called before the first frame update
    void Start()
    {
        theTime = 120f;
        transform.rotation = Quaternion.Euler(0,0,0);
        opensfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        theTime -= Time.deltaTime;

        if(-3 > theTime && theTime > -4){
            transform.rotation = Quaternion.Euler(0,0,-90);
            opensfx.clip = Open; 
            if(!opensfx.isPlaying){
            opensfx.Play();
            }
        }
    }
}
