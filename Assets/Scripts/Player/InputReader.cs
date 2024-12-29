using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader
{
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _rollAction;
    private InputAction _toggleRunning;
    
    private Vector2 _moveInput;
    public bool _isJumping;
    private bool _isRolling;
    private bool _runToggle = true;

    public Vector3 MoveVector => new Vector3(_moveInput.x, 0, _moveInput.y);
    public bool IsJumping => _isJumping;
    public bool IsRolling => _isRolling;
    public bool RunToggled => _runToggle;

    public InputReader(PlayerInput playerInput, float movementDeadZone)
    {
        _moveAction = playerInput.Game.Move;
        _jumpAction = playerInput.Game.Jump;
        _toggleRunning = playerInput.Game.ToggleRunning;

        _moveAction.performed += ctx =>
        {
            _moveInput = ctx.ReadValue<Vector2>();
            if (_moveInput.magnitude < movementDeadZone)
            {
                _moveInput = Vector2.zero;
            }
        };

        _moveAction.canceled += ctx => _moveInput = Vector2.zero;

        _jumpAction.performed += ctx => _isJumping = true;
        _jumpAction.canceled += ctx => _isJumping = false;

        _rollAction.performed += ctx => _isRolling = true;
        _rollAction.canceled += ctx => _isRolling = false;

        _toggleRunning.performed += ctx => _runToggle = !_runToggle;
    }
}
