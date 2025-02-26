using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public SpaceClass currentSpace;
    private int spaceToMove = 0;
    public  bool block = true;
    public bool playerAction = false;
    
    public TurnController turnController;
    public int money = 0;
    public int point = 0;

    public Dice dice;

    public SpaceClass makeChoice(ArrayList nextSpaces)
    {
        return (SpaceClass)nextSpaces[0];
    }

    public void moveNSpaces(int n)
    {
        StartCoroutine(RollDiceThenMove());
    }

    private void Update()
    {
        // Move player smoothly to current space
        Vector3 tempPos = currentSpace.transform.position;
        Quaternion tempRot = Quaternion.Euler(0.0f, 180, 0);
        Quaternion tempRot0 = Quaternion.Euler(0.0f, 0, 0);


        tempPos.y += 0.5f;
        if (!turnController.isMyTurn(this))
        {
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, tempRot, Time.deltaTime * 1);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos + new Vector3(turnController.calculateOffSet(this), 0, 0), 1 * Time.deltaTime);


        }
        else {
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, tempRot0, Time.deltaTime * 1);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos, 1 * Time.deltaTime);

        }

        if (Input.GetKeyUp(KeyCode.Space) && block == false)
        {
            StartCoroutine(RollDiceThenMove());
        }
    }

    IEnumerator RollDiceThenMove()
    {
        block = true;
        dice.gameObject.SetActive(true);

        dice.StartRolling();

     
        yield return new WaitForSeconds(2.0f); // Adjust based on dice animation duration

       
        int ran = UnityEngine.Random.Range(1, 7);
        print("Rolled " + ran);

       
        dice.StopRolling(ran);

       
        yield return new WaitForSeconds(1.0f);
        dice.gameObject.SetActive(false);

       
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
