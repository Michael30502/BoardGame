using UnityEngine;

public class SplitPath : MonoBehaviour, SpaceActions
{
    public SpaceClass leftSpace;
    public SpaceClass rightSpace;

    [SerializeField] private GameObject pathSelectionCanvas; 
    private Player currentPlayer; 

    public void action(Player player)
    {
        currentPlayer = player; 
        if (pathSelectionCanvas != null)
        {
            pathSelectionCanvas.SetActive(true); 
        }
    }

    public bool getCountSpace()
    {
        return false;
    }

    public void OnLeftPathButtonClick()
    {
        if (currentPlayer != null)
        {
            currentPlayer.currentSpace.nextSpaces = leftSpace;
            print("Left path selected");
            CompleteSelection();
        }
    }

   
    public void OnRightPathButtonClick()
    {
        if (currentPlayer != null)
        {
            currentPlayer.currentSpace.nextSpaces = rightSpace;
            print("Right path selected");
            CompleteSelection();
        }
    }

    
    private void CompleteSelection()
    {
        if (pathSelectionCanvas != null)
        {
            pathSelectionCanvas.SetActive(false); // Hide UI canvas
        }

        if (currentPlayer != null)
        {
            currentPlayer.playerAction = false; // Allow player to continue
        }
    }
}
