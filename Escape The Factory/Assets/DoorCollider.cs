using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{

    float theTime;

    // Start is called before the first frame update
    void Start()
    {
        theTime = 120f;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;  
    }

    // Update is called once per frame
    void Update()
    {
        theTime -= Time.deltaTime;

        if(theTime <= -3f){
            theTime = -3f; 
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
