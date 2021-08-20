using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Flowchart))]
public class NPCController : MonoBehaviour
{
        [SerializeField]
        string message = "";
        GameObject player3Obj;
        Flowchart flowChart;
        void Start()
        {
            player3Obj = GameObject.FindGameObjectWithTag("Player");
            flowChart = GetComponent<Flowchart>();
        }
        private void OnCollisionEnter2D(UnityEngine.Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Talk());
            }
        }
        IEnumerator Talk()
        {
            flowChart.SendFungusMessage(message);
            yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        }
    
}
