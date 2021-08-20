
using UnityEngine;

public class enemymove : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameObject DesEffect;

    Rigidbody2D rigidbody2D;
    float speed = 0;

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.RIGHT;


    


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        direction = DIRECTION_TYPE.RIGHT;
    }


    private void Update()
    {
        if (!IsGround())
        {
            ChangeTurn();
        }
        if (IsBlock())
        {
            ChangeTurn();
        }
        
    }

    public void EnemyDestroy()
    {
        
        Instantiate(DesEffect, this.transform.position, this.transform.rotation);

        //消す動作
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 2;
                transform.localScale = new Vector3(1, 1, 1);

                break;
            case DIRECTION_TYPE.LEFT:
                speed = -2;
                transform.localScale = new Vector3(-1, 1, 1);

                break;

        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.5f;
        
        
        Debug.DrawLine(startVec, endVec);
        
        return Physics2D.Linecast(startVec, endVec, blockLayer);
    }

    //壁に当たったのを検知する関数
    bool IsBlock()
    {
        Vector3 WidthVec = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 end2Vec = WidthVec - transform.right * 0.2f;
        Debug.DrawLine(WidthVec, end2Vec);
        return Physics2D.Linecast(WidthVec, end2Vec, blockLayer);

    }

    void ChangeTurn()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }
    
}
