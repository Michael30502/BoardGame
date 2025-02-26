using UnityEngine;

public class PointSpace : MonoBehaviour, SpaceActions
{
    [SerializeField] private GameObject VictoryPointMenu;
    private Player currentPlayer; 

    private int pointPrice = 5; 

    public void action(Player player)
    {
        if (VictoryPointMenu != null)
        {
            currentPlayer = player; 
            VictoryPointMenu.SetActive(true); 
        }
    }

    
    public void OnYesButtonClick()
    {
        if (currentPlayer != null && currentPlayer.money >= pointPrice)
        {
            currentPlayer.money -= pointPrice;
            currentPlayer.point += 1;
            print("You got a point!");
        }
        else
        {
            print("You cannot afford a point.");
        }

        CloseMenu();
    }

    
    public void OnNoButtonClick()
    {
        print("You chose not to buy a point.");
        CloseMenu();
    }

   
    private void CloseMenu()
    {
        if (VictoryPointMenu != null)
        {
            VictoryPointMenu.SetActive(false);
        }

        if (currentPlayer != null)
        {
            currentPlayer.playerAction = false; // Allow the player to continue
        }
    }

    public bool getCountSpace()
    {
        return false;
    }
}
