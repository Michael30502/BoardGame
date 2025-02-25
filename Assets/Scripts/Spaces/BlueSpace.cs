using UnityEngine;

public class BlueSpace : MonoBehaviour,SpaceActions
{









    public void action(Player player) {

        player.money += 1;

        
    
    
    }

    public bool getCountSpace()
    {
        return true;
    }
}
