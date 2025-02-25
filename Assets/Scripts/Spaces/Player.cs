using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public SpaceClass currentSpace;
    private int spaceToMove = 0;
    private bool block = false;
    public bool playerAction = false;

    public int money = 0;

    public Dice dice;  // Reference to the Dice script

    public SpaceClass makeChoice(ArrayList nextSpaces)
    {
        return (SpaceClass)nextSpaces[0];
    }

    public void moveNSpaces(int n)
    {
        StartCoroutine(SwapSpace(n)); 
    }

    private void Update()
    {
        // Move player smoothly to current space
        Vector3 tempPos = currentSpace.transform.position;
        tempPos.y += 0.5f;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos, 1 * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Space) && block == false)
        {
            int ran = UnityEngine.Random.Range(1, 7);
            print("Rolled " + ran);

            dice.StartRolling();
            StartCoroutine(SwapSpace(ran));
            block = true;
        }
    }

    IEnumerator SwapSpace(int n)
    {
       //roll first, then walk anima
        yield return new WaitForSeconds(1.0f);
        dice.StopRolling(n); 
        yield return new WaitForSeconds(0.5f);
        while (n > 0)
        {
            if (!playerAction) { 
            currentSpace = currentSpace.nextSpaces;
            yield return new WaitForSeconds(1.5f);

            if (currentSpace.spaceAction.getCountSpace())
            {
                n--;
            }
            else
            {
                    print("check");
                currentSpace.spaceAction.action(this);
                playerAction = true;
            }
        }
            yield return new WaitForSeconds(1);



        }
        block = false;
        print("Ready current money: "+ money);
    }
}
