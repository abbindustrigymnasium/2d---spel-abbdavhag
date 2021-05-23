using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByBox : MonoBehaviour
{
    AudioSource audioSrc;
    public AudioClip Death;
    float waiter;
    bool haveHitBox;

    void Start(){
        audioSrc = GetComponent<AudioSource>();
        waiter = 0.6f;
        haveHitBox = false;
    }

    void Update(){
        if(haveHitBox){
            waiter -= Time.deltaTime;
            if(waiter <= 0){
                SceneManager.LoadScene(3);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            audioSrc.clip = Death;
            haveHitBox = true;
            if(!audioSrc.isPlaying){
                audioSrc.Play();
            }
	    }
    }
}
