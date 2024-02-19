using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputData))]
public class SchmirbyMovement : MonoBehaviour
{
    [SerializeField] private TiltMove _tiltMoveReference;
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 4.0f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;
    public AudioSource audioSource;
    public AudioClip jump;
    private InputData _inputData;
    private Vector3 _headsetRotation;
    private float gravity = -6.8f;
    private Vector3 movement;
    private int jumpsTotal = 3;
    private int jumpCounter = 0; 
    private float jumpCooldown = 0.25f;
    private float lastUpdateTime;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    void Update()
    {
        //is schmirby on the floor?
        bool _isGrounded = IsGrounded();

        //left and right controller actions
        _inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity);
        _inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity);

        //input checks for jumps
        if (JumpCooldown(leftVelocity, rightVelocity) && (_isGrounded || jumpCounter < jumpsTotal))
        {
            Jump();
            audioSource.PlayOneShot(jump);
            //Debug.Log(jumpCounter);
        }

        //input checks for headset tilts
        if (_inputData._HMD.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion headsetRotation)){
            
            // Convert the quaternion to Euler angles in headset's local space
            Vector3 localEulerAngles = headsetRotation.eulerAngles;

            //Check the tilt around the local x-axis (forward/backward)
            if (localEulerAngles.x > 180)
                localEulerAngles.x -= 360;

            //Check the tilt around the local z-axis (left/right)
            if (localEulerAngles.z > 180)
                localEulerAngles.z -= 360;

            if(localEulerAngles.x > 5.0){
                //moves schmirby forward
                if(!IsGrounded() || IsWaddle(leftVelocity, rightVelocity))
                    _tiltMoveReference.inputY = 1;
            }else if (localEulerAngles.x < -5.0){
                //moves schmirby backward
                if(!IsGrounded() || IsWaddle(leftVelocity, rightVelocity))
                    _tiltMoveReference.inputY = -1;
            }else{
                //dont move schmirby forward or backward
                _tiltMoveReference.inputY = 0;
            }
            if(localEulerAngles.z > 5.0){
                //moves schmirby left
                if(!IsGrounded() || IsWaddle(leftVelocity, rightVelocity))
                    _tiltMoveReference.inputX = -1;
            }else if (localEulerAngles.z < -5.0){
                //moves schmirby right
                if(!IsGrounded() || IsWaddle(leftVelocity, rightVelocity))
                    _tiltMoveReference.inputX = 1;
            }else{
                //dont move side to side
                _tiltMoveReference.inputX = 0;
            }
        }

        //gravity update
        movement.y += gravity * Time.deltaTime;
        cc.Move(movement * Time.deltaTime);
    }

    private bool JumpCooldown(Vector3 left, Vector3 right)
    {
        if (Time.time - lastUpdateTime >= jumpCooldown){
            if(left.y > 1.0f && right.y > 1.0){
                lastUpdateTime = Time.time;
                return true;
            }
            return false;
        }
        return false;
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -1.0f * gravity);
        UpdateJumpCounter();
    }

    private bool IsWaddle(Vector3 left, Vector3 right)
    {
        //tracks the z-axis velocity of controllers
        float zLVelocity = left.z;
        float zRVelocity = right.z;

        if(Mathf.Abs(zLVelocity) > 0.05f && Mathf.Abs(zRVelocity) > 0.05f){
            //Debug.Log("Waddling On");
            return true;
        }else{
            //Debug.Log("Waddling Off");
            _tiltMoveReference.inputY = 0;
            _tiltMoveReference.inputX = 0;
            return false;
        }
    }

    private bool IsGrounded()
    {
        if(Physics.CheckSphere(cc.transform.position, 0.4f, groundLayers)){
            jumpCounter = 0;
            return true;
        }else{
            return false;
        }
    }

    private void UpdateJumpCounter()
    {
        jumpCounter++;
    }
}
