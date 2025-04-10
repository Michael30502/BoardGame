using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Internal.Commands;

public class Player : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public SpaceClass currentSpace;
    private int spaceToMove = 0;
    public int extraSpacesToMove;
    public  bool block = true;
    public bool playerAction = false;

    
    public TurnController turnController;
    public int money = 5;
    public int point = 0;
    public int defaultDieType = 6;

    public int dieType = 6;
    public Dice dice;
    public Dice defaultdice;

    public Gamepad gamepad;

    public SpaceClass makeChoice(ArrayList nextSpaces)
    {
        return (SpaceClass)nextSpaces[0];
    }

    public void moveNSpaces(int n)
    {
        StartCoroutine(RollDiceThenMove());
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        // Move player smoothly to current space
        Vector3 tempPos = currentSpace.transform.position;
        Quaternion tempRot = Quaternion.Euler(0.0f, 180, 0);
        Quaternion tempRot0 = Quaternion.Euler(0.0f, 0, 0);

        //Removes the players of a space, when it is not their turn
        tempPos.y += 0.5f;
        if (!turnController.isMyTurn(this))
        {
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, tempRot, Time.deltaTime * 1);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos + new Vector3(turnController.calculateOffSet(this), 0, 0), 1 * Time.deltaTime);


        }
        else
        {
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, tempRot0, Time.deltaTime * 1);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos, 1 * Time.deltaTime);

        }
        if (items.Count == 0) { 
        if (InputManager.InputSelect(gamepad) && block == false)
        {
            StartCoroutine(RollDiceThenMove());
        }
            
    }
        else if (!block)
        {
            StartCoroutine(ChooseItem());
        }
    }

    IEnumerator ChooseItem()
    {
        int itemSelected = 0;
        block = true;
        print("choose item");
        while (true)
        {
            bool test = false;
            if (InputManager.InputLeft(gamepad) )
            {
                itemSelected--;
                test = true;
            }
            if (InputManager.InputRight(gamepad) )
            {
                itemSelected++;
                test = true;
            }

            if(itemSelected> items.Count-1)
            {
                itemSelected = 0;
            } else if (itemSelected < 0)
            {
                itemSelected = items.Count - 1;
            }

            if (test)
            {
                print(items[itemSelected].Name);

            }

            if (InputManager.InputSelect(gamepad))
            {
                print("test2");
                items[itemSelected].Action(this);
                items.RemoveAt(itemSelected);
                yield return StartCoroutine(RollDiceThenMove());
                break;

                
            }
            if (InputManager.InputCancel(gamepad))
            {
                yield return StartCoroutine(RollDiceThenMove());
                break;


            }

            yield return null;
        }


    }

    IEnumerator RollDiceThenMove()
    {
        block = true;
        Dice dieObject = GameObject.Instantiate(dice);
        dieObject.player = transform;

        dieObject.StartRolling();

     
        yield return new WaitForSeconds(2.0f); // Adjust based on dice animation duration

       
        int ran = UnityEngine.Random.Range(1, dieType+1);
        print("Rolled " + ran);


        dieObject.StopRolling(ran);

       
        yield return new WaitForSeconds(1.0f);
        DestroyImmediate(dieObject.gameObject);
        dice = defaultdice;
        dieType = defaultDieType;
       
        yield return StartCoroutine(SwapSpace(ran));

       
    }

    

    IEnumerator SwapSpace(int n)
    {
        while (n > 0)
        {
            if (!playerAction)
            {
                currentSpace = currentSpace.nextSpaces;
                yield return new WaitForSeconds(1.5f);

                if (currentSpace.spaceAction.getCountSpace())
                {
                    n--;
                }
                else
                {
                    print("check");
                    playerAction = true;
                    currentSpace.spaceAction.action(this);
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }

        currentSpace.spaceAction.action(this);
        yield return new WaitForSeconds(2);
        print("Ready current money: "+ money);
        turnController.currentPlayer++;
        turnController.changePlayerTurn();


    }
}
