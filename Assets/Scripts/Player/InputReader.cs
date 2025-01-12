using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private float movementDeadZone;

    [Header("Events")]
    [SerializeField] private Vector3EventChannel onMoveEventChannel;
    [SerializeField] private EventChannelSO onJumpEventChannel;
    [SerializeField] private EventChannelSO onRollEventChannel;
    [SerializeField] private EventChannelSO onAttackEventChannel;
    [SerializeField] private EventChannelSO onLockEventChannel;

    private Vector2 _moveInput;
    private bool _runToggle = true;

    public Vector3 MoveVector => new Vector3(_moveInput.x, 0, _moveInput.y).normalized;

    private void Awake()
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
            onMoveEventChannel.RaiseEvent(MoveVector);
        };

        playerInput.Game.Move.canceled += ctx =>
        {
            _moveInput = Vector2.zero;
            onMoveEventChannel.RaiseEvent(MoveVector);
        };

        playerInput.Game.Jump.performed += ctx =>
        {
            onJumpEventChannel.RaiseEvent();
        };

        playerInput.Game.Roll.performed += ctx =>
        {
            onRollEventChannel.RaiseEvent();
        };

        playerInput.Game.Lock.performed += ctx =>
        {
            onLockEventChannel.RaiseEvent();
        }

        playerInput.Game.ToggleRunning.performed += ctx => _runToggle = !_runToggle;
    }
}
