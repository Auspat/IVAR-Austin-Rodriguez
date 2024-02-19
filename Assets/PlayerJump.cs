using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 0.25f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;

    private float gravity = -4.8f;
    private Vector3 movement;
    private int jumpsTotal = 4;
    private int jumpCounter = 0; 

    private void Update(){
        bool _isGrounded = IsGrounded();
        if (jumpButton.action.WasPressedThisFrame() && (_isGrounded || jumpCounter < jumpsTotal)){
            Jump();
        }
        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);
    }

    private void Jump(){
        movement.y = Mathf.Sqrt(jumpHeight * -1.0f * gravity);
        UpdateJumpCounter();
    }

    private bool IsGrounded(){
        if(Physics.CheckSphere(cc.transform.position, 0.4f, groundLayers)){
            jumpCounter = 0;
            return true;
        }else{
            return false;
        }
    }

    private void UpdateJumpCounter(){
        jumpCounter++;
    }
}
