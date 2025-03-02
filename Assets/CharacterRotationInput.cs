using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; 

    void Update()
    {
        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.Q))
            rotationInput = -1f; 
        if (Input.GetKey(KeyCode.E))
            rotationInput = 1f; 

        
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f, Space.World);
    }
}
