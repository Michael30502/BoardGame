using UnityEngine;

public class BlueSpace : MonoBehaviour,SpaceActions
{


    public GameObject dollarPrefab;






    public void Action(Player player) {

        player.money += 1;

        print("You recieved 1 money!");
        if (dollarPrefab != null)
        {
            GameObject dollar = Instantiate(dollarPrefab, player.transform.position, Quaternion.identity);
            
        }

    }

    public bool GetCountSpace()
    {
        return true;
    }
}
