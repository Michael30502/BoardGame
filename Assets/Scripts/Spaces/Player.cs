using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public SpaceClass currentSpace;
    private bool block = false;
    public bool playerAction = false;

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
        tempPos.y += 0.5f;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos, 1 * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Space) && block == false)
        {
            StartCoroutine(RollDiceThenMove());
        }
    }

    IEnumerator RollDiceThenMove()
    {
        block = true;
        dice.StartRolling();

     
        yield return new WaitForSeconds(2.0f); // Adjust based on dice animation duration

       
        int ran = UnityEngine.Random.Range(1, 7);
        print("Rolled " + ran);

       
        dice.StopRolling(ran);

       
        yield return new WaitForSeconds(1.0f);
        dice.gameObject.SetActive(false);

       
        yield return StartCoroutine(SwapSpace(ran));

       
        dice.gameObject.SetActive(true);
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
        block = false;
        print("Ready current money: " + money);
    }
}
