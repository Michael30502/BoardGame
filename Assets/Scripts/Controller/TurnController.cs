using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : MonoBehaviour
{
    [SerializeField] public Player[] playerList;
    [SerializeField] public GameObject gameModeSelectionMenuUI;
    [SerializeField] public GameObject trophyRoom;
    [SerializeField] private Transform trophySpot;

    [SerializeField] private List<GameObject> itemMenus;

    public HUDManager hudManager;
    public Camera cameras;

    public int turn = 1;
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

        gameModeSelectionMenuUI.SetActive(false);
        setAllGamepads();

        try
        {
            GameOptions gameOptions = GameObject.Find("GameOptions").GetComponent<GameOptions>();
            if (gameOptions != null)
            {
                for (int i = 0; i < gameOptions.players.Count; i++)
                {
                    Destroy(playerList[i].transform.GetChild(0).gameObject);
                    GameObject playerInstance = Instantiate(gameOptions.players[i]);
                    playerInstance.transform.SetParent(playerList[i].gameObject.transform, false);
                }
            }
        }
        catch
        {
            Debug.Log("GameOptions not found");
        }
    }

    void Update() { }

    public void setAllGamepads()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            if (Gamepad.all.Count > i)
            {
                playerList[i].gamepad = Gamepad.all[i];
            }
        }
    }

    public void changePlayerTurn()
    {
        
        foreach (var player in playerList)
        {
            player.block = true;
        }

        //menu troubleshooting basically
        foreach (var menu in itemMenus)
        {
            if (menu != null)
                menu.SetActive(false);
        }

        // Determine active player
        int index = currentPlayer - 1;
        if (index < 0 || index >= playerList.Length) return;

        Player activePlayer = playerList[index];
        activePlayer.block = false;
        cameras.player = activePlayer.transform;

        // Enable only this player's menu if they have items
        if (activePlayer.items.Count > 0 && itemMenus[index] != null)
        {
            itemMenus[index].SetActive(true);
            Debug.Log($"Player {activePlayer.id}'s turn. Menu enabled.");
        }

        // Edge case: round end
        if (currentPlayer == 5)
        {
            turn++;
            changeTurn();
            if (!gameOver && gameModeSelectionMenuUI != null)
                gameModeSelectionMenuUI.SetActive(true);
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
                        Player playerScript = root.GetComponentInChildren<Player>();
                        if (playerScript != null)
                        {
                            playerScript.enabled = false;
                            Debug.Log("Winner: Player " + root.name);
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

    private IEnumerator SnapToTrophyAfterDelay(Transform playerTransform, Vector3 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerTransform.position = targetPosition;
        Debug.Log("Winner transferred successfully.");
    }

    public bool isMyTurn(Player player)
    {
        return currentPlayer == checkPlayerNumber(player);
    }

    public void TrophyRoomEnablement()
    {
        if (gameOver)
        {
            gameModeSelectionMenuUI.SetActive(false);
            trophyRoom.SetActive(true);
            cameras.gameObject.SetActive(false);
        }
    }

    public float calculateOffSet(Player player)
    {
        int count = 0;
        List<bool> list = new List<bool>(new bool[4]);

        for (int i = 0; i < 4; i++)
        {
            if (player.currentSpace == playerList[i].currentSpace && (i + 1 != currentPlayer))
            {
                count++;
                list[i] = true;
            }
        }

        int playerNo = checkPlayerNumber(player);
        if (count > 1)
        {
            switch (playerNo)
            {
                case 1: count = 1; break;
                case 2: count = (!list[0]) ? 1 : count; break;
                case 3: count = (!list[0] && !list[1]) ? 1 : (list[0] || list[1]) ? 2 : count; break;
                case 4:
                    if (!list[0] && !list[1] && !list[2]) count = 1;
                    else if ((!list[0] && !list[1]) || (!list[0] && !list[2]) || (!list[1] && !list[2])) count = 2;
                    else if (list[0] || list[1] || list[2]) count = 3;
                    break;
            }
        }

        return 1f * count;
    }

    public void DisableCurrentMenu()
    {
        int index = currentPlayer - 1;
        if (index >= 0 && index < itemMenus.Count && itemMenus[index] != null)
        {
            itemMenus[index].SetActive(false);
        }
    }


    public int checkPlayerNumber(Player player)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (player == playerList[i - 1])
                return i;
        }
        return -1;
    }

    public void updatePlayerGuide() { }
}
