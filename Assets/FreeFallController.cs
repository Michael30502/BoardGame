using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFallController : MonoBehaviour
{
    [SerializeField] private FreeFallCharacter[] playerList;

    void Start()
    {
        AssignGamepads();
    }

    private void AssignGamepads()
    {
        int gamepadIndex = 0; // Keep track of assigned gamepads

        foreach (var player in playerList)
        {
           
            if (gamepadIndex < Gamepad.all.Count)
            {
                player.SetGamepad(Gamepad.all[gamepadIndex]); // Assign gamepad in order
                gamepadIndex++; // Move to the next available gamepad
            }
            else
            {
                player.SetGamepad(null); // No gamepad available
            }
        }
    }
}
