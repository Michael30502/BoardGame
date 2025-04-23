using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    [SerializeField] public List<Door> doors;
    [SerializeField] public List<Key> keys;
    [SerializeField] public List<PlayerMinigame3> players;
    public bool active = false;
    public int turn = 1;
    public int currentPlayer = 0;
    public int playersLeft = 4;
    public string scene;
    private List<int> playerOrder = new List<int>();
    [SerializeField] private MinigameManager minigameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        try
        {
            minigameManager = GameObject.Find("MinigameController").GetComponent<MinigameManager>();
            if (minigameManager != null)
            {
                minigameManager.mainSceneParent.SetActive(false);
            }
        }
        catch(System.Exception e) { print("main scene not found"); }

        for (int i = 0; i < doors.Count; i++) {


            while (doors[i].key == null) {

                int rdmNo = Random.Range(0, 5);
                print(rdmNo);
                if (!keys[rdmNo].active)
                {
                    doors[i].key = keys[rdmNo];
                    keys[rdmNo].active = true;
                }
                    
                    }

        }

        players[0].takeAction();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool checkKeyPair(Door door, Key key)
    {
        bool result= false;
        if (door.key == key)
        {
            print("correct key");
            doors.Remove(door);
            keys.Remove(key);
            door.gameObject.SetActive(false);
            key.gameObject.SetActive(false);
            players[currentPlayer].hasWon = true;
            playerOrder.Add(currentPlayer);
            playersLeft--;
            result = true;
        }
        else
            print("incorrect key");

        changePlayerTurn();

        return result;


    }
    public void changePlayerTurn()
    {
     
        currentPlayer++;
        if (currentPlayer > players.Count - 1)
        {
            currentPlayer = 0;
        }

        while (true) { 
        if (!players[currentPlayer].hasWon)
        {
            players[currentPlayer].takeAction();
            break;
        }
            else
            {
                currentPlayer++;
                print(currentPlayer);

                if ( currentPlayer > players.Count-1)
                {
                    currentPlayer = 0;
                }
            }

            }
        if (playersLeft == 1)
        {
            playerOrder.Add(currentPlayer);
            

            minigameManager.mainSceneParent.SetActive(true);
            minigameManager.SetPlayerMoney(playerOrder);
            SceneManager.UnloadSceneAsync(scene);
           
        }

    }


}
