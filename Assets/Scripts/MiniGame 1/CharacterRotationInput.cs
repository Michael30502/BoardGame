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

        if (assignedGamepad != null)
        {
            float rightStickX = assignedGamepad.rightStick.x.ReadValue();

            // Some controllers do shitty-stickdrift, so this is what we call a hotfix 
            if (Mathf.Abs(rightStickX) > 0.1f)
            {
                rotationInput = rightStickX;
            }
        }

      
        if (assignedGamepad == null) 
        {
            if (Input.GetKey(KeyCode.Q))
                rotationInput -= 1f;
            if (Input.GetKey(KeyCode.E))
                rotationInput += 1f;
        }

        // Y axis spin-move!
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f, Space.World);
    }
}
