using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SplitPath : MonoBehaviour,SpaceActions
{
    public SpaceClass leftSpace;
    public SpaceClass rightSpace;










    public void action(Player player)
    {
        StartCoroutine(makeChoice(player));
        print("make selection");

    }

    public bool getCountSpace()
    {
        return false;
    }

    IEnumerator makeChoice(Player player) {
        print("check2");

        bool block = true;
        bool leftPathSelected = true;
        while (block)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                leftPathSelected = true;
                print("left path selected");

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                leftPathSelected = false;
                print("right path selected");
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (!leftPathSelected)
                {
                    player.currentSpace.nextSpaces = rightSpace;
                }
                else { 
                    player.currentSpace.nextSpaces = leftSpace;
            }
                block = false;
                player.playerAction = false;
                break;
            }

            yield return null;


        }

    }

}
