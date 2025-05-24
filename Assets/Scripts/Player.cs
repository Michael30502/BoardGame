using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour, IComparable<Player>
{
    public List<Item> items = new List<Item>();
    public SpaceClass currentSpace;
    public int extraSpacesToMove;
    public bool block = true;
    public bool playerAction = false;

    public TurnController turnController;
    public int money = 5;
    public int point = 0;
    public int id = 0;
    public int position = 0;
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

    public int CompareTo(Player other)
    {
        if (point != other.point) return other.point.CompareTo(point);
        if (money != other.money) return other.money.CompareTo(money);
        return id.CompareTo(other.id);
    }

    private void Update()
    {
        Vector3 tempPos = currentSpace.transform.position;
        Quaternion tempRot = Quaternion.Euler(0.0f, 180, 0);
        Quaternion tempRot0 = Quaternion.Euler(0.0f, 0, 0);
        tempPos.y += 0.5f;

        bool isMyTurn = turnController.isMyTurn(this);

        if (!isMyTurn)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tempRot, Time.deltaTime * 1);
            transform.position = Vector3.Lerp(transform.position, tempPos + new Vector3(turnController.calculateOffSet(this), 0, 0), 1 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tempRot0, Time.deltaTime * 1);
            transform.position = Vector3.Lerp(transform.position, tempPos, 1 * Time.deltaTime);

            
            if (items.Count == 0 && !block)
            {
                if (InputManager.InputSelect(gamepad))
                    StartCoroutine(RollDiceThenMove());
            }
            else if (!block)
            {
                StartCoroutine(ChooseItem());
            }
        }
    }



    IEnumerator ChooseItem()
    {
        block = true;
        print("choose item");
        int itemSelected = 0;

        while (true)
        {
            int tempValue = ChooseOption.Choose(itemSelected, items.Count, gamepad);
            if (itemSelected != tempValue)
            {
                itemSelected = tempValue;
                print(items[itemSelected].name);
            }

            if (InputManager.InputSelect(gamepad))
            {
                items[itemSelected].Action(this);
                items.RemoveAt(itemSelected);
                turnController.DisableCurrentMenu(); // Hides menu
                yield return StartCoroutine(RollDiceThenMove());
                break;
            }

            if (InputManager.InputCancel(gamepad))
            {
                turnController.DisableCurrentMenu(); // Hides menu
                yield return StartCoroutine(RollDiceThenMove());
                break;
            }

            yield return null;
        }
    }

    IEnumerator RollDiceThenMove()
    {
        block = true;

        turnController.DisableCurrentMenu();

        Dice dieObject = GameObject.Instantiate(dice);
        dieObject.player = transform;

        dieObject.StartRolling(dieType);


        yield return new WaitForSeconds(2.0f); // Adjust based on dice animation duration


        int ran = UnityEngine.Random.Range(1, dieType + 1);

        print("Rolled " + ran);



        dieObject.StopRolling(ran,dieType);

        
        ran += extraSpacesToMove;
        if (extraSpacesToMove != 0)
        {
            print("player moves an additional " + extraSpacesToMove + "spaces");
        }
        extraSpacesToMove = 0;


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

                if (currentSpace.spaceAction.GetCountSpace())
                    n--;
                else
                {
                    playerAction = true;
                    currentSpace.spaceAction.Action(this);
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }

        currentSpace.spaceAction.Action(this);
        yield return new WaitForSeconds(2);

        turnController.currentPlayer++;
        turnController.changePlayerTurn();
    }
}
