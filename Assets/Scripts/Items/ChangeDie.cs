using UnityEngine;

public class ChangeDie : Item
{

    public Dice die;
    public int dieType;
   
   public override void Action(Player player)
    {

        player.dice = die;
        player.dieType = dieType;
    }





}
