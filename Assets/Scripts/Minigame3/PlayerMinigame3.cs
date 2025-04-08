using System.Collections;
using UnityEditor.Animations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMinigame3 : MonoBehaviour
{
    Gamepad gamePad = null;
    [SerializeField] public Manager manager;

    public bool hasWon=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(!hasWon)
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        else
            gameObject.GetComponentInChildren<Animator>().enabled = true;

        //print(transform.rotation);
        Quaternion tempRot = Quaternion.Euler(0.0f, 180, 0);
        Quaternion tempRot0 = Quaternion.Euler(0.0f, 0, 0);
        //print(this);
        //print(manager.players[manager.currentPlayer - 1] == this);
        if (manager.players[manager.currentPlayer] == this)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tempRot, Time.deltaTime * 1);

        }
        else
        {
           transform.rotation = Quaternion.Slerp(transform.rotation, tempRot0, Time.deltaTime * 1);


        }

    }

    public void takeAction()
    {
         StartCoroutine(pickKey());
    }



    IEnumerator pickKey() {
        Input.ResetInputAxes();

        int selection = 0;
        bool block = true;
        while (block)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {

                selection--;


            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                selection++;


            }





            if (selection > manager.keys.Count-1)
            {
                selection = 0;
            }
            if (selection < 0)
            {
                selection = manager.keys.Count - 1;
            }

            for (int i = 0; i < manager.keys.Count; i++)
            {
                if (selection == i)
                    manager.keys[i].selected = true;
                else
                    manager.keys[i].selected = false;

            }

            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                Input.ResetInputAxes();

                block = false;
           }
            yield return null;

        }

        yield return StartCoroutine(pickDoor(selection));

    }

    IEnumerator pickDoor(int keySelection)
    {
        Input.ResetInputAxes();

        int selection = 0;
        bool block = true;
        while (block)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {

                selection--;


            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                selection++;


            }





            if (selection > manager.doors.Count - 1)
            {
                selection = 0;
            }
            if (selection < 0)
            {
                selection = manager.doors.Count - 1;
            }

            for (int i = 0; i < manager.doors.Count; i++)
            {
                if (selection == i)
                    manager.doors[i].selected = true;
                else
                    manager.doors[i].selected = false;

            }
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                manager.checkKeyPair(manager.doors[selection], manager.keys[keySelection]);
                manager.doors[selection].selected = false;
                manager.keys[keySelection].selected = false;
                block = false;
                yield return new WaitForSeconds(1.0f);

            }
            yield return null;

        }

        yield return null;

    }
}



