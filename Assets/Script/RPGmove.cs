using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * キャラの座標を変更するController
 */
public class RPGmove : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField]
    float SPEED = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    [SerializeField]
    public FixedJoystick joystick;
    void Start()
    {
        // オブジェクトに設定しているRigidbody2Dの参照を取得する
        this.rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        this.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Update()
    {
        // x,ｙの入力値を得る
        inputAxis.x = joystick.Horizontal;
        inputAxis.y = joystick.Vertical;
    }
    private void FixedUpdate()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * SPEED;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FirstStage")
        {
            Debug.Log("ファーストステージに飛びます");
            gameManager.MoveToFirst();
            //GameManagerファイルのGameOver()を実行する
            
        }
        if (collision.gameObject.tag == "SecondStage")
        {
            Debug.Log("セカンドステージに飛びます");
            gameManager.MoveToSecond();
            

        }
    }
}


