using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemsget : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        //Gameobjectをヒエラルキーシーンの中から見つける
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void GetItem()
    {
        gameManager.AddScore(100);
        Destroy(this.gameObject);
    }
}
