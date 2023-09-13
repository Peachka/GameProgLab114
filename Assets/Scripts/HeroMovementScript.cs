using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 26f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool hasJumped = false;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            hasJumped = false; // Reset the flag when grounded
        }

        if (Input.GetButtonDown("Jump") && !hasJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            hasJumped = true; // Set the flag after jumping
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
