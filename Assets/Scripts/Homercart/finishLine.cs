using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class finishLine : MonoBehaviour
{

    public GameObject[] finishCanvas;
    private List<int> playerRank = new List<int>(); 
    private int counter;
    private MinigameManager minigameManager;

    void Start()
    {
        foreach (GameObject canvas in finishCanvas)
        {
            canvas.SetActive(false);
        }
    }
    void Update()
    {
        if (counter >= 3)
        {

            try
            {
                minigameManager = GameObject.Find("MinigameController").GetComponent<MinigameManager>();

            }
            catch (System.Exception e) { print("main scene not found"); }
            if (minigameManager!=null) { 
            Debug.Log("All players have finished!");
            int[] playerNumbers = { 0, 1, 2, 3 };
            for (int i = 0; i < playerNumbers.Length; i++)
            {
                if (!playerRank.Contains(playerNumbers[i]))
                {


                    playerRank.Add(i);

                    break;
                }
                }
                Scene scene = SceneManager.GetSceneByName("Homercart");
                minigameManager.mainSceneParent.SetActive(true);
                minigameManager.SetPlayerMoney(playerRank);
                SceneManager.UnloadSceneAsync(scene);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (counter < 3) { 
        if (other.CompareTag("Player1"))
        {
            Debug.Log("Player1 triggered finish!");
            counter++;
                if (!playerRank.Contains(0))
                    playerRank.Add(0);
            finishCanvas[0].SetActive(true);
        }
        if (other.CompareTag("Player2"))
        {
            Debug.Log("Player2 triggered finish!");

            counter++;
                if (!playerRank.Contains(1))
                    playerRank.Add(1);

                finishCanvas[1].SetActive(true);

        }
        if (other.CompareTag("Player3"))
        {
            Debug.Log("Player3 triggered finish!");

            counter++;
                if (!playerRank.Contains(2))
                    playerRank.Add(2);

                finishCanvas[2].SetActive(true);
        }
        if (other.CompareTag("Player4"))
        {
            Debug.Log("Player4 triggered finish!");

            counter++;
                if(!playerRank.Contains(3))
                playerRank.Add(3);

                finishCanvas[3].SetActive(true);
        }
    }
    }


}

