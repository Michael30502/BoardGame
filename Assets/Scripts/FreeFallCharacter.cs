using UnityEngine;

public class FreeFallCharacter : MonoBehaviour
{
    public float fallSpeed = 20f; 
    public float moveSpeed = 10f; 
    public float gravity = 15f; 

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Input fra player
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical"); 

       
        moveDirection.x = moveX * moveSpeed;
        moveDirection.z = moveZ * moveSpeed;
        moveDirection.y = -fallSpeed - gravity * Time.deltaTime; 

      
        controller.Move(moveDirection * Time.deltaTime);
    }
}
