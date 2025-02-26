using UnityEngine;

public class RedSpace : MonoBehaviour,SpaceActions
{



    public GameObject dollarPrefab;
    public GameObject fireVFXPrefab; // Fire VFX prefab (VFX_Fire_01_Big)




    public void action(Player player) {

        player.money -= 1;
        if (player.money < 0) {
            player.money = 0;
        }

        print("You lost 1 money!");
        if (dollarPrefab != null)
        {
            // Spawn the dollar at the player's position
            GameObject dollar = Instantiate(dollarPrefab, player.transform.position, Quaternion.identity);

            // **Attach fire VFX to the dollar**
            if (fireVFXPrefab != null)
            {
                GameObject fireVFX = Instantiate(fireVFXPrefab, dollar.transform.position, Quaternion.identity);

                // Make the fire VFX a child of the dollar so it stays attached
                fireVFX.transform.SetParent(dollar.transform);
            }
        }


    }

    public bool getCountSpace()
    {
        return true;
    }
}
