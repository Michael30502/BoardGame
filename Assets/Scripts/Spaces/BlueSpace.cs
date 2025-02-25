using UnityEngine;

public class BlueSpace : MonoBehaviour,SpaceActions
{









    public void action(Player player) {

        player.money += 1;

        print("You recieved 1 money!");


    }

    public bool getCountSpace()
    {
        return true;
    }
}
