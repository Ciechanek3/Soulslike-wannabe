using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader
{
    private Vector2 _moveInput;
    public bool _isJumping;
    private bool _isRolling;
    private bool _runToggle = true;

    public Vector3 MoveVector => new Vector3(_moveInput.y, 0, _moveInput.x).normalized;
    public bool IsJumping => _isJumping;
    public bool IsRolling => _isRolling;
    public bool RunToggled => _runToggle;

    public InputReader(Vector3EventChannel onMoveEvent, EventChannelSO onJumpEvent, float movementDeadZone)
    {
        var playerInput = new PlayerInput();
        playerInput.Game.Enable();
        playerInput.Game.Move.performed += ctx =>
        {
            
            _moveInput = ctx.ReadValue<Vector2>();
            if (_moveInput.magnitude < movementDeadZone)
            {
                _moveInput = Vector2.zero;
            }
            onMoveEvent.RaiseEvent(MoveVector);
        };

        playerInput.Game.Move.canceled += ctx =>
        {
            _moveInput = Vector2.zero;
            onMoveEvent.RaiseEvent(MoveVector);
        };

        playerInput.Game.Jump.performed += ctx =>
        {
            _isJumping = true;
            onJumpEvent.RaiseEvent();
        };
        playerInput.Game.Jump.canceled += ctx => _isJumping = false;

        playerInput.Game.Roll.performed += ctx => _isRolling = true;
        playerInput.Game.Roll.canceled += ctx => _isRolling = false;

        playerInput.Game.ToggleRunning.performed += ctx => _runToggle = !_runToggle;
    }
}
