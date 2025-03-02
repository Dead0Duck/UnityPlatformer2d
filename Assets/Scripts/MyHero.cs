// Kartavov 119 PKS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHero : MonoBehaviour
{
    const float groundCheckRadius = 0.2f;
    public Transform groundCheckCollider;
    public LayerMask groundLayer;

    public float Jump = 8f;
    public float Speed = 5f;

    public LayerMask deathLayer;
    public GameObject deathPanel;
    public LayerMask winLayer;
    public GameObject winPanel;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Speed;
        animator.SetFloat("w", Mathf.Abs(moveX));
        sprite.flipX = (moveX < 0);

        bool ground = IsGrounded();
        animator.SetInteger("h", ground ? 0 : 1); // � ����� ������� �������� ����������, �� ���������, � ������ �������.

        float moveY = rb.velocity.y;
        if (moveY <= .1 && ground && Input.GetAxis("Vertical") > 0)
        {
            moveY += Jump;
        }
        rb.velocity = new Vector2(moveX, moveY);


        if (IsTriggeringDeath())
        {
            Camera.main.transform.parent = null;
            deathPanel.SetActive(true);
            Object.Destroy(gameObject);
        }
        else if (IsTriggeringWin())
        {
            Camera.main.transform.parent = null;
            winPanel.SetActive(true);
            Object.Destroy(gameObject);
        }
    }

    bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        return false;
    }

    bool IsTriggeringDeath()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, deathLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        return false;
    }

    bool IsTriggeringWin()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, winLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        return false;
    }
}
