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

    private List<IInputObserver> _inputObservers = new List<IInputObserver>();

    public Vector3 MoveVector => new Vector3(_moveInput.x, 0, _moveInput.y);
    private Vector3 oldVector;
    public bool IsJumping => _isJumping;
    public bool IsRolling => _isRolling;
    public bool RunToggled => _runToggle;

    public InputReader(PlayerInput playerInput, float movementDeadZone)
    {
        playerInput.Game.Move.performed += ctx =>
        {
            _moveInput = ctx.ReadValue<Vector2>();
            if (_moveInput.magnitude < movementDeadZone)
            {
                _moveInput = Vector2.zero;
            }
            NotifyObservers();
        };

        playerInput.Game.Move.canceled += ctx =>
        {
            _moveInput = Vector2.zero;
            NotifyObservers();
        };

        playerInput.Game.Jump.performed += ctx => _isJumping = true;
        playerInput.Game.Jump.canceled += ctx => _isJumping = false;

        playerInput.Game.Roll.performed += ctx => _isRolling = true;
        playerInput.Game.Roll.canceled += ctx => _isRolling = false;

        playerInput.Game.ToggleRunning.performed += ctx => _runToggle = !_runToggle;
    }

    public void RegisterObserver(IInputObserver observer)
    {
        _inputObservers.Add(observer);
    }

    public void UnregisterObserver(IInputObserver observer)
    {
        _inputObservers.Remove(observer);
    }

    private void NotifyObservers()
    {
        if (oldVector == MoveVector) return;

        oldVector = MoveVector;

        foreach (var observer in _inputObservers)
        {
            observer.OnInputChanged(MoveVector);
        }
    }
}
