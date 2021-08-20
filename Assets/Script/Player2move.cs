using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//この記述はしなくても動作するがバグが発生する可能性を取り除くため
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player2move : MonoBehaviour
{


    [SerializeField] Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpForce;
    [SerializeField] private GameManager gameManager;

    public Text GameOver;
    public Text GameClear;

    private bool IsJumping = false;

    void Update()
    {
        if (Input.GetKeyDown("space") && !IsJumping)
        {
            Jump();
        }
        //rigidbodyで新しく取得し、Vector2(x, y)
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        IsJumping = true;
        //ForceMode2D.Impulseを引数に与えると瞬間的に力を加えます
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //On~Enter2Dは〜にぶつかった処理を実行する
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Stage"))
        {
            IsJumping = false;
        }
    }

    //こっちはトリガーで考えた
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("GameOver！");

            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("GameClear");

            gameManager.GameClear();
        }
    }

}