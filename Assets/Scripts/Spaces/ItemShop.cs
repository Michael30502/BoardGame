using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemShop : MonoBehaviour, SpaceActions
{

    public List<Item> shop = new List<Item>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Action(Player player)
    {

        StartCoroutine(Shop(player));



    }

    public bool GetCountSpace()
    {
        return false;
    }


    IEnumerator Shop(Player player) {
        print("Buy Something!!!");
        int itemSelected = 0;


        while (true) {
            int tempValue = ChooseOption.Choose(itemSelected, shop.Count, player.gamepad);
            if (itemSelected != tempValue)
        {
            itemSelected = tempValue;
            print(shop[itemSelected].name + " " + shop[itemSelected].price+"$");
        }

            if (ChooseOption.Select(player.gamepad))
            {
                if (player.money >= shop[itemSelected].price)
                {
                    player.items.Add((Item)shop[itemSelected].Clone());
                    player.playerAction = false;
                    player.money-= shop[itemSelected].price;
                    print("You bought: " + shop[itemSelected].name+" Thank you come again");
                    break;
                }
                else {
                    print("No money");
                }

              

            }

            if (ChooseOption.Cancel(player.gamepad))
            {
                player.playerAction = false;
                break;

            }


            yield return null;

    }


}

}
