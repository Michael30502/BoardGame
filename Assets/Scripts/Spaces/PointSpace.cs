using System.Collections;
using UnityEngine;

public class PointSpace : MonoBehaviour,SpaceActions
{









    public void action(Player player) {

        StartCoroutine(makeChoice(player));


    }

    public bool getCountSpace()
    {
        return false;
    }

    IEnumerator makeChoice(Player player)
    {
        int pointprice = 5;

        print("Buy point for: " +pointprice+" money y/n");

        bool block = true;
        

        while (block)
        {

            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (player.money >= pointprice)
                {
                    player.money -= pointprice;
                    player.point += 1;
                    print("You Got a Point!");
                    break;
                }
                else {
                    print("You Cannot afford a point");
                    break;
                }

            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                break;            }



            yield return null;


        }
        player.playerAction = false;



    }


}
