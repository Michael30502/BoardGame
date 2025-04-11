using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Gamepad assignedGamepad = null;

    public void SetGamepad(Gamepad gamepad)
    {
        assignedGamepad = gamepad;
    }


    void Update()
    {
        float rotationInput = 0f;

        // Controller input
        if (assignedGamepad != null)
        {
            // Read right stick horizontal input
            float rightStickX = assignedGamepad.rightStick.ReadValue().x;

            // Optional: Add a small deadzone to prevent drift
            if (Mathf.Abs(rightStickX) > 0.1f)
            {
                rotationInput += rightStickX;
            }
        }

        // Keyboard input
        if (Input.GetKey(KeyCode.Q))
            rotationInput -= 1f;
        if (Input.GetKey(KeyCode.E))
            rotationInput += 1f;

        // Apply rotation
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f, Space.World);
    }
}
