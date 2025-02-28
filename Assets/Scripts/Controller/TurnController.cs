using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TurnController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public Player[] playerList;
    [SerializeField] public GameObject gameModeSelectionMenuUI;

    public Camera cameras;

    public int turn =0;
    public int currentPlayer = 1;

    void Start()
    {
        print("test2");
        if (gameModeSelectionMenuUI != null)
        {
            gameModeSelectionMenuUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

     

        
    }




    public void changePlayerTurn() {

        switch (currentPlayer)
        {
            case 1:
                cameras.player = playerList[0].transform;

                playerList[0].block = false;

                break;
            case 2:
                cameras.player = playerList[1].transform;

                playerList[1].block = false;

                break;
            case 3:
                cameras.player = playerList[2].transform;

                playerList[2].block = false;

                break;
            case 4:
                cameras.player = playerList[3].transform;

                playerList[3].block = false;

                break;
            case 5:
                turn++;
                changeTurn();

                // Enable game mode selection UI when a round has passed
                if (gameModeSelectionMenuUI != null)
                {
                    gameModeSelectionMenuUI.SetActive(true);
                }
                break;
        }

    }


    public void changeTurn() {
        currentPlayer = 1;
        changePlayerTurn();
    
    }

    public bool isMyTurn(Player player) {

        return currentPlayer == checkPlayerNumber(player);


    }

    public float calculateOffSet(Player player) {
        int count = 0;
        List<bool> list = new List<bool>(new bool[4]);
        for (int i = 0; i <4; i++)
        {
            if (player.currentSpace == playerList[i].currentSpace&&( i+1 !=currentPlayer))
            {
                count++;
                list[i] = true;
            }
        }
        print(list[0] + " " + list[1] + " " + list[2] + " " + list[3]);

        int playerNo =checkPlayerNumber(player);
        if (count > 1)
        {
            switch (playerNo)
            {

                case 1:
                    count = 1;
                    break;
                case 2:
                    if (!list[0])
                    {
                        count = 1;
                    }


                    break;
                case 3:
                    if (!list[0] && !list[1])
                    {
                        count = 1;
                    }
                    else if (list[0]|| list[1])
                    {
                        count = 2;
                    }
                    break;
                case 4:
                    if (!list[0] && !list[1] && !list[2])
                    {
                        count = 1;
                    }
                    else if ((!list[0] && !list[1])||(!list[0] && !list[2])|| (!list[1] && !list[2]))
                    {
                        count = 2;
                    }
                    else if (list[0]|| list[1]|| list[2])
                    {
                        count = 3;
                    }
                    break;
            }

        }
            return 1f * count;

        
    }

    public int checkPlayerNumber(Player player) {

        for (int i = 1; i<=4; i++) {
        if(player == playerList[i - 1])
            {
                return i;
            }
        }
        return -1;

       
    }


}
