using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;






public class car_controller : MonoBehaviour
{

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    [SerializeField] private raceCountDown match;
    public Gamepad gamepad;
    private float carindex;
Rigidbody rb;

void Start()
{
    rb = GetComponent<Rigidbody>();
    rb.centerOfMass = new Vector3(0, -0.9f, 0); // Helps prevent flipping
}

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
 


     private void GetInput() {
        // Steering Input
        float horizontal = 0f;
        float vertical = 0f;
        
        if (InputManager.InputRight(gamepad))
            horizontal += 1f;
        if (InputManager.InputLeft(gamepad))
            horizontal -= 1f;
        horizontalInput = horizontal;


        if (InputManager.InputSelect(gamepad) && match.isMatch)
        {
            match.isMatch = true;
        }
        


        if ( InputManager.InputReverse(gamepad) && match.isMatch)
        {
            vertical = -0.6f; 
        }
        else if ( InputManager.InputSelect(gamepad) && match.isMatch)
        {
            vertical = 1f; // forward
        }
        else if ((InputManager.InputReverse(gamepad) ||InputManager.InputSelect(gamepad)) && match.isMatch)
            vertical = 0.03f;
        else
        {
            vertical = 0; // race is over
        }


            
 

        verticalInput = vertical;


        isBreaking = InputManager.InputCancel(gamepad);
        if(InputManager.flipOver(gamepad) && match.isMatch)
        
        {
            flipCar();
        }
         }  
        
      
        
    

    private void HandleMotor() {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

private void flipCar()
{
Vector3 fwd = transform.forward;
transform.rotation = Quaternion.identity;
transform.forward = fwd;
}

}