using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float rollingDuration = 0.01f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool rolling = false;
    private Animator animator;
    private float timeRollingCountdown = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!rolling)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            horizontalMove = controller.IsFacingRight() ? runSpeed : -runSpeed;
        }

        if (Input.GetButtonDown("Jump") && !rolling)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Roll") && !rolling)
        {
            rolling = true;
            animator.SetBool("IsRolling", true);
            timeRollingCountdown = rollingDuration;
        }
        else if (rolling && timeRollingCountdown < 0)
        {
            rolling = false;
            animator.SetBool("IsRolling", false);
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsJumping", !controller.IsGrounded());
        if (rolling)
        {
            timeRollingCountdown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, rolling, jump);
        jump = false;
    }

}