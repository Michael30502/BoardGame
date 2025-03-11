using System.Collections;
using UnityEngine;

public class SplitPath : MonoBehaviour, SpaceActions
{
    public SpaceClass leftSpace;
    public SpaceClass rightSpace;

    [SerializeField] private GameObject pathSelectionCanvas;
    private Player currentPlayer;
    private bool block;

    public void action(Player player)
    {
        currentPlayer = player;
        if (pathSelectionCanvas != null)
        {
            pathSelectionCanvas.SetActive(true);
        }
        StartCoroutine(makeChoice(player));
    }

    public bool getCountSpace()
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





            if (Input.GetKeyDown(KeyCode.LeftArrow)|| (player.gamepad.dpad.left.isPressed)||player.gamepad.leftStick.left.isPressed)


            {


                leftPathSelected = true;
                //TODO insert outline here (left)


                print("left path selected");





            }


            if (Input.GetKeyDown(KeyCode.RightArrow) || (player.gamepad.dpad.right.isPressed)|| player.gamepad.leftStick.right.isPressed)


            {

                //TODO insert outline here (right)

                leftPathSelected = false;


                print("right path selected");


            }





            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)||(player.gamepad.buttonSouth.isPressed))


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