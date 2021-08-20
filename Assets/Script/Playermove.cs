
using UnityEngine;
using UnityEngine.UI;

public class Playermove : MonoBehaviour
{
    //こうすることで他のファイルを読み込む事ができる
    [SerializeField] GameManager gameManager;
    
    [SerializeField] AudioClip ItemGetSE;
    [SerializeField] AudioClip EnemyClashSE;
    [SerializeField] AudioClip JumpSE;

    AudioSource audioSource;

    //[SerializeField]でunity画面での設定が可能
    [SerializeField] LayerMask blockLayer;

    //移動をする上で基本となる記述
    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    //初期の方向状態
    DIRECTION_TYPE deirection = DIRECTION_TYPE.STOP;

    float speed = 0;

    Animator animator;

    bool IsDeath = false;

    //rigidbody を宣言
    Rigidbody2D rigidbody2D;
    
    

    //飛ぶ力宣言
    float jumpPower = 300;



    private void Start()
    {
        //スタートの時点で自分についてるrigidbodyを取得している
        rigidbody2D = GetComponent<Rigidbody2D>();
        //アニメーターを取得する
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    public void Leftderiection()
    {
        deirection = DIRECTION_TYPE.LEFT;
        transform.localScale = new Vector2(-1, 1);
    }

    public void Rightderiection()
    {
        //xが＋つまり右に行く時
        deirection = DIRECTION_TYPE.RIGHT;
        //transformのScale[local]は分からないがとりま下記のようにする
        transform.localScale= new Vector2(1, 1);
    }



    private void Update()
    {
        

        if (IsDeath)
        {
            return;
        }
        //横方向の動き取得キーボードだからinput
        float x = Input.GetAxis("Horizontal");

        //パラメーターがfloatなので
        animator.SetFloat("speed", Mathf.Abs(x));

        if (x == 0)
        {
            //xが０だったらストップ
            deirection = DIRECTION_TYPE.STOP;
            
        }
        else if (x > 0)
        {
            Rightderiection();
        }
        else if (x < 0)
        {
            Leftderiection();
        }
        //地面についているかいないか
        if (IsGround())
        {
            //GetKeyDownで押し込んだ時Upで話した時
            if (Input.GetKeyDown("space"))
            {
                Jump();
            }
            else
            {
                animator.SetBool("IsJump", false);
            }
        }
    }

    

    public void rigidbodyveloctiy()
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    //ちょくちょく更新する時に使う
    private void FixedUpdate()
    {
        if (IsDeath)
        {
            return;
        }
        switch (deirection)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3;

                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                
                break;
            
        }
        //rigidbodyのinfoで使われている「velocity」を呼び出してVector2(xの値,yの値)
        rigidbodyveloctiy();
    }



    public void Jump()
    {
        //rigidbodyの上に力を加える関数AddForce()でVector2=(x,y)*jumpPower
        rigidbody2D.AddForce(Vector2.up * jumpPower);

        animator.SetBool("IsJump", true);

        audioSource.PlayOneShot(JumpSE); 
    }

    //地面についているかいないかをboolで求めている
    bool IsGround()
    {
        //左側の線
        Vector3 leftStartPosition = transform.position - Vector3.right * 0.2f;
        //右側の線
        Vector3 rightStartPosition = transform.position + Vector3.right * 0.2f;
        //まとめる線
        Vector3 endPoint = transform.position - Vector3.up * 0.1f;

        //視点と終点をデバックで視聴可能になる
        Debug.DrawLine(leftStartPosition, endPoint);
        Debug.DrawLine(rightStartPosition, endPoint);

        //支店、終点、どのオブジェクト今回は地面blockなので
        return Physics2D.Linecast(leftStartPosition, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartPosition, endPoint, blockLayer);
    }

    //unity既存の変数　２Dの物にぶつかった時の処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDeath)
        {
            return;
        }
        //unityの「Tag」
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("ゲームオーバー");
            PlayerDeath();
            //GameManagerファイルのGameOver()を実行する
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("ゲームクリア");
            gameManager.GameClear();
        }

        if (collision.gameObject.tag == "Item")
        { 
            collision.gameObject.GetComponent<Itemsget>().GetItem();
            audioSource.PlayOneShot(ItemGetSE);
        }

        if (collision.gameObject.tag == "SecondStage")
        { 
            gameManager.MoveToSecond();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            //わざわざ読んでくる必要がないcollisionの場合は
            enemymove enemy = collision.gameObject.GetComponent<enemymove>();

            //上から踏んだ場合
            if (this.transform.position.y + 0.2f > enemy.transform.position.y)
            {
                //ここで一旦スピードを0にする
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                audioSource.PlayOneShot(EnemyClashSE);

                //敵を踏んだらジャンプさせる
                Jump();
                enemy.EnemyDestroy();
            }
            else
                //横からぶつかったら
            {
                PlayerDeath();
                gameManager.GameOver();
            }
        }

    }
    void PlayerDeath()
    {
        IsDeath = true;
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        animator.Play("Death Animation");
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);
        gameManager.GameOver();
    }
}
