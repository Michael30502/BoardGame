using UnityEngine;

public class AddValueToDie : Item
{
    public int value;

    public override void Action(Player player)
    {
        player.extraSpacesToMove = value;


    }



}
