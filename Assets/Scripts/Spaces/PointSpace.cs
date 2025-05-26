using System.Collections;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class PointSpace : MonoBehaviour, SpaceActions
{
    [SerializeField] private GameObject VictoryPointMenu;
    private Player currentPlayer;
    private bool block = false;
    private int pointPrice = 5;

    public void Action(Player player)
    {
        if (VictoryPointMenu != null)
        {
            currentPlayer = player;
            VictoryPointMenu.SetActive(true);
        }
        StartCoroutine(makeChoice(player));

    }


    public void OnYesButtonClick()
    {
        if (currentPlayer != null && currentPlayer.money >= pointPrice)
        {
            currentPlayer.money -= pointPrice;
            currentPlayer.point += 1;
            print("You got a point!");
        }
        else
        {
            print("You cannot afford a point.");
        }

        CloseMenu();
    }


    public void OnNoButtonClick()
    {
        print("You chose not to buy a point.");
        CloseMenu();
    }


    private void CloseMenu()
    {
        if (VictoryPointMenu != null)
        {
            VictoryPointMenu.SetActive(false);
        }

        if (currentPlayer != null)
        {
            block = false; //Ends the loop for selection
            currentPlayer.playerAction = false; // Allow the player to continue
        }
    }

    public bool GetCountSpace()
    {
        return false;
    }

    IEnumerator makeChoice(Player player)
    {


        print("check2");





        block = true;


        bool leftPathSelected = true;
        //TODO insert outline here (left)


        while (block)


        {





            if (InputManager.InputLeft(player.gamepad))


            {


                leftPathSelected = true;
                //TODO insert outline here (left)


                print("left path selected");





            }


            if (InputManager.InputRight(player.gamepad))


            {

                 
                leftPathSelected = false;
                //TODO insert outline here (right)

                print("right path selected");


            }





            if (InputManager.InputSelect(player.gamepad))


            {


                if (leftPathSelected)


                {


                    OnYesButtonClick();


                }
                else
                {

                    OnNoButtonClick();

                }


                block = false;


                player.playerAction = false;


                break;


            }

            yield return null;

        }
    }
}
