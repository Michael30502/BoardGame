using UnityEngine;

public class ChangeDie : MonoBehaviour,Item
{

    Dice die;
    public new string name;
    public string Name {
        get { return name; }
        set { name = value; }
    }

    void action(Player player)
    {
        player.dice = die;

    }





}
