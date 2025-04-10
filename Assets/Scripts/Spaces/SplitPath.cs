using System.Collections;
using UnityEngine;

public class SplitPath : MonoBehaviour, SpaceActions
{
    public SpaceClass leftSpace;
    public SpaceClass rightSpace;

    [SerializeField] private GameObject pathSelectionCanvas;
    private Player currentPlayer;
    private bool block;

    public void Action(Player player)
    {
        currentPlayer = player;
        if (pathSelectionCanvas != null)
        {
            pathSelectionCanvas.SetActive(true);
        }
        StartCoroutine(makeChoice(player));
    }

    public bool GetCountSpace()
    {
        return false;
    }

    public void OnLeftPathButtonClick()
    {
        if (currentPlayer != null)
        {
            currentPlayer.currentSpace.nextSpaces = leftSpace;
            print("Left path selected");
            CompleteSelection();
        }
    }


    public void OnRightPathButtonClick()
    {
        if (currentPlayer != null)
        {
            currentPlayer.currentSpace.nextSpaces = rightSpace;
            print("Right path selected");
            CompleteSelection();
        }
    }


    private void CompleteSelection()
    {
        if (pathSelectionCanvas != null)
        {
            pathSelectionCanvas.SetActive(false); // Hide UI canvas
        }

        if (currentPlayer != null)
        {
            block = false;
            currentPlayer.playerAction = false; // Allow player to continue
        }
    }


    IEnumerator makeChoice(Player player)
    {


        print("check2");





        block = true;

        //TODO insert outline here (left)

        bool leftPathSelected = true;


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

                //TODO insert outline here (right)

                leftPathSelected = false;


                print("right path selected");


            }





            if (InputManager.InputSelect(player.gamepad))


            {


                if (!leftPathSelected)


                {


                    OnRightPathButtonClick();


                }
                else
                {

                    OnLeftPathButtonClick();

                }


                block = false;


                player.playerAction = false;


                break;


            }





            yield return null;








        }


    }
}