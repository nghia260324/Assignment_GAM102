using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAutoMove : MonoBehaviour
{
    public BoxCollider2D boxCollider2DJump;
    public BoxCollider2D boxCollider2DFall;
    public BoxCollider2D boxCollider2DGround;
    public CapsuleCollider2D capsuleCollider2D;

    public LayerMask ground;
    public Transform body;

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D m_Rigidbody2D;

    private float currentSpeed;
    private bool isJump = false;
    private bool isFacingRight = false;
    private Animator m_Animator;



    private void Start()
    {
        currentSpeed = moveSpeed;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsGroundFall())
        {
            if (IsGroundJump())
            {
                Flip();
            } else
            {
                if (IsGround())
                {
                    m_Rigidbody2D.velocity = Vector2.up * jumpForce;
                }
            }
        } else
        {
            Flip();
        }
        m_Rigidbody2D.velocity = new Vector2(-currentSpeed, m_Rigidbody2D.velocity.y);
        m_Animator.SetBool("isRunning", true);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = body.localScale;
        localScale.x *= -1;
        body.transform.localScale = localScale;
        currentSpeed *= -1;
    }
    private bool IsGround()
    {
        return Physics2D.BoxCast(boxCollider2DGround.bounds.center, boxCollider2DGround.bounds.size, 0f, Vector2.right, 0.1f, ground);
    }
    private bool IsGroundJump()
    {
        return Physics2D.BoxCast(boxCollider2DJump.bounds.center, boxCollider2DJump.bounds.size, 0f, Vector2.right, 0.1f, ground);
    }
    private bool IsGroundFall()
    {
        return Physics2D.BoxCast(boxCollider2DFall.bounds.center, boxCollider2DFall.bounds.size, 0f, Vector2.right, 0.1f, ground);
    }
}
