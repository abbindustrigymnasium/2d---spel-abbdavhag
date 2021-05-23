using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public Animator animator;
    public float moveSpeed = 3;
    public CharacterController2D controller;

    float horizontal = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        }
    }

    public void onLanding()
    {
        animator.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}
