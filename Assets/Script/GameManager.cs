using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //gamepverのテキストを表示させる
    public GameObject gameover;
    public GameObject gameclear;
    public GameObject first;
    public GameObject second;
    [SerializeField] Text scoreText;


    [SerializeField] AudioClip gameOverSE;
    [SerializeField] AudioClip gameClearSE;
    AudioSource audioSource;

    //ここでスコアの値を定義している　
    const int Max_Score = 9999;
    int score = 0;



    //val（値と入れることでいくつでも変更可能
    public void AddScore(int val)
    {
        score += val;
        if (score > Max_Score)
        {
            score = Max_Score;
        }
        scoreText.text = score.ToString();
    }


    private void Start()
    {
        
        
        audioSource = GetComponent<AudioSource>();
    }


    public void GameOver()
    { 
        
        gameover.SetActive(true);
        //SE音を挿入
        audioSource.PlayOneShot(gameOverSE);
        //Invokeで難病にリスタート実行
        Invoke("Restart", 2f);
    }

    public void GameClear()
    {
        gameclear.SetActive(true);
        audioSource.PlayOneShot(gameClearSE);
       
        //ゲームクリアで次の画面に移動する
        Invoke("next", 3f);
        

    }

    public void MoveToFirst()
    {
        first.SetActive(true);

        Invoke("FirstStage", 3f);

    }

    public void MoveToSecond()
    {
        second.SetActive(true);

        Invoke("SecondStage", 3f);

    }

    //リスタート
    void Restart()
    {
        //現在のシーンを取得して
        Scene thisScene = SceneManager.GetActiveScene();
        //現在の現在のシーンを呼び出す
        SceneManager.LoadScene(thisScene.name);
    }

    //ネクストステージ
    void next()
    {
        
        SceneManager.LoadScene("SecondStage");
    }

    void FirstStage()
    {
        SceneManager.LoadScene("PlayerScence");
    }

    void SecondStage()
    {
        SceneManager.LoadScene("SecondStage");
    }
}


