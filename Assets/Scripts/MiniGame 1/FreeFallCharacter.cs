using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFallCharacter : MonoBehaviour
{
    public float fallSpeed = 100f;
    public float moveSpeed = 10f;
    public float gravity = 9.81f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Vector3 moveDirection;
    private Gamepad assignedGamepad = null;

    public void SetGamepad(Gamepad gamepad)
    {
        assignedGamepad = gamepad;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        rb.useGravity = false;  // We manually handle gravity
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (assignedGamepad != null) // ONLY use the assigned gamepad
        {
            moveX = assignedGamepad.leftStick.x.ReadValue();
            moveZ = assignedGamepad.leftStick.y.ReadValue();
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }

        Vector3 inputVector = new Vector3(moveX, 0, moveZ);
        if (inputVector.magnitude > 1f)
            inputVector.Normalize();

        moveDirection = inputVector * moveSpeed;
        

        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y - (gravity * Time.fixedDeltaTime), moveDirection.z);
    }
}
