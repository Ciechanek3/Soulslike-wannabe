using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private float movementDeadZone; // for controllers only

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private bool isJumping;

    public Vector3 MoveInput => new Vector3(moveInput.x, 0, moveInput.y);
    public bool IsJumping => isJumping;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.Game.Move;
        jumpAction = playerInput.Game.Jump;

        moveAction.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector2>();
            if (moveInput.magnitude < movementDeadZone)
            {
                moveInput = Vector2.zero;
            }
        };

        moveAction.canceled += ctx => moveInput = Vector2.zero;

        jumpAction.performed += ctx => isJumping = true;
        jumpAction.canceled += ctx => isJumping = false;
    }

}
