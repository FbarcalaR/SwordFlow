using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public int health = 10;

    [HideInInspector]
    public bool facingLeft = false;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private Rigidbody2D rb2d;
    private Animator animator;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Roll"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Roll"))
        {
            crouch = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        animator.SetBool("IsJumping", !controller.isGrounded());
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(bloodEffect, transfor.position, Quaternion.identity);
        health -= damage;
    }
}