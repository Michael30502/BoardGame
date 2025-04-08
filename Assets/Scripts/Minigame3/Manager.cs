using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{

    [SerializeField] public List<Door> doors;
    [SerializeField] public List<Key> keys;
    [SerializeField] public List<PlayerMinigame3> players;
    public bool active = false;
    public int turn = 1;
    public int currentPlayer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

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
    public void checkKeyPair(Door door, Key key)
    {
        if (door.key == key)
        {
            print("correct key");
            doors.Remove(door);
            keys.Remove(key);
            door.gameObject.SetActive(false);
            key.gameObject.SetActive(false);
            players[currentPlayer].hasWon = true;

        }
        else
            print("incorrect key");

        changePlayerTurn();

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

    }


}
