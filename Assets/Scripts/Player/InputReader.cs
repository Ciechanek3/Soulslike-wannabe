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
    [SerializeField] private Vector3EventChannel onLockEventChannel;

    private Vector2 _moveInput;
    private Vector3 _cameraInput;
    private bool _runToggle = true;

    public Vector3 MoveVector => new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
    public Vector3 CameraVector => new Vector3(_cameraInput.x, _cameraInput.y, 0);

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

        playerInput.Game.Look.performed += ctx =>
        {
            _cameraInput = ctx.ReadValue<Vector2>();
            if (_cameraInput.magnitude < movementDeadZone)
            {
                _cameraInput = Vector2.zero;
            }
            //onLockEventChannel.RaiseEvent(CameraVector);  
        };

        playerInput.Game.Look.canceled += ctx =>
        {
            _cameraInput = Vector2.zero;
            //onLockEventChannel.RaiseEvent(CameraVector);
        };

        playerInput.Game.ToggleRunning.performed += ctx => _runToggle = !_runToggle;
    }
}
