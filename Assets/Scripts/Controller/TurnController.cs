using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public Player[] playerList;
    [SerializeField] public GameObject gameModeSelectionMenuUI;
    [SerializeField] public GameObject trophyRoom;

    [SerializeField] private Transform trophySpot;



    public HUDManager hudManager;



    public Camera cameras;


    public int turn =1;
    public int currentPlayer = 1;
    public int maxRounds = 1;
    public bool gameOver = false;

    void Start()
    {
        hudManager.ChangeRound(turn, maxRounds);


        playerList[0].id = 1;
        playerList[1].id = 2;
        playerList[2].id = 3;
        playerList[3].id = 4;

        {
            gameModeSelectionMenuUI.SetActive(false);
        }
        setAllGamepads();
        GameOptions gameOptions = null;
        try{ 
        gameOptions = GameObject.Find("GameOptions").GetComponent<GameOptions>();
        }
        catch { print("gameoption not found");}
        if (gameOptions != null) {
            print(gameOptions.players.Count);
            for (int i = 0; i < gameOptions.players.Count; i++) {
                Destroy(playerList[i].transform.GetChild(0).gameObject);
                GameObject playerInstance = Instantiate(gameOptions.players[i]);
                playerInstance.transform.SetParent(playerList[i].gameObject.transform, false);
      
                }
        }
    }

   
    void Update()
    {

     

        
    }

    public void setAllGamepads()
    {
        for( int i = 0; i < playerList.Length; i++) {
            if (Gamepad.all.Count >i)
            {
                playerList[i].gamepad = Gamepad.all[i];
            }
        }
}



    public void changePlayerTurn() {

        switch (currentPlayer)
        {
            case 1:
                cameras.player = playerList[0].transform;

                playerList[0].block = false;
                print("player 1s turn");

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

                //To account for Roundbreak & gameOver, so menu dont pop up when its over..
                if (!gameOver && gameModeSelectionMenuUI != null)
                {
                    gameModeSelectionMenuUI.SetActive(true);
                }
                break;

        }

    }

    public void changeTurn()
    {
        if (turn > maxRounds)
        {
            gameOver = true;

            Player winner = hudManager.GetTopPlayer();
            if (winner != null)
            {
                foreach (var root in playerList)
                {
                    if (root.GetComponentInChildren<Player>() == winner)
                    {
                        
                        root.transform.position = Vector3.zero;

                        //This is because playerscript forces the space to be linked, so its a fix to workaround it to display winner.
                        Player playerScript = root.GetComponentInChildren<Player>();
                        if (playerScript != null)
                        {
                            playerScript.enabled = false;
                            Debug.Log(" Winner Player nr. " + root.name);
                        }

                        
                        StartCoroutine(SnapToTrophyAfterDelay(root.transform, trophySpot.position, 1f));
                        break;
                    }
                }
            }


            gameModeSelectionMenuUI.SetActive(false);
            TrophyRoomEnablement();
        }

        else
        {
            currentPlayer = 1;
            hudManager.ChangeRound(turn, maxRounds);
        }
    }
    //Transfer to trophy
    private IEnumerator SnapToTrophyAfterDelay(Transform playerTransform, Vector3 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerTransform.position = targetPosition;
        Debug.Log(" Winner transfered succesuflly.");
    }





    public bool isMyTurn(Player player) {

        return currentPlayer == checkPlayerNumber(player);


    }
    public void TrophyRoomEnablement()
    {
        if (gameOver == true)
        {
            gameModeSelectionMenuUI.SetActive(false);
            trophyRoom.gameObject.SetActive(true);
            cameras.gameObject.SetActive(false);
        }
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
        //print(list[0] + " " + list[1] + " " + list[2] + " " + list[3]);

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
      public void updatePlayerGuide()
    {
    
    for (int i = 0; i < playerList.Length; i++)
    {
    
    }
    
}
    


}
