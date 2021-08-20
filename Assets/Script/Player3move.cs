using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]


public class Player3move : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpForce;

    

    Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Atack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    void FixedUpdate()
        {
        //スピードだからfloat でHorizontal
        float x = Input.GetAxis("Horizontal");

        //パラメーターがfloatなので
        animator.SetFloat("Speed", Mathf.Abs(x));

        if (x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }
    
    public void Atack()
    {
        animator.SetTrigger("IsAttack");
        
    }

    public void Jump()
    {
        
        //ForceMode2D.Impulseを引数に与えると瞬間的に力を加えます
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }



    

}
