using UnityEngine;

public class RedSpace : MonoBehaviour,SpaceActions
{









    public void action(Player player) {

        player.money -= 1;
        if (player.money < 0) {
            player.money = 0;
        }
        
    
    
    }

    public bool getCountSpace()
    {
        return true;
    }
}
