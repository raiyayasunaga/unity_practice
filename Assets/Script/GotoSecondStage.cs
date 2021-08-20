using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GotoSecondStage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
