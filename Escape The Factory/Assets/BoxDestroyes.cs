using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyes : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip Impact;
    	
    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Falldetector"){
			Destroy(gameObject);
		}
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BoxConveyor")
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Impact;
            audioSource.Play();
        }
    }
}
