using System.Collections.Generic;
using UnityEngine;
 
public class MinigameManager : MonoBehaviour
{

    public List<Player> playerList = new List<Player>();
    public int[]  moneyTable = {5,3,1,0};

    private bool minigamePlayed;
    public GameObject mainSceneParent;
    public TurnController turnController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerMoney(List<int> playerOrder) {

        for (int i = 0;i < playerList.Count;i++) {

            playerList[playerOrder[i]].money += moneyTable[i];
        }
        turnController.changePlayerTurn();
    
    }




}
