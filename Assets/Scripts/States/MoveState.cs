using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState, IMovementModel
{
    private Transform _cameraTransform;

    private float _movementSpeed;
    private float _jumpSpeed;

    private Vector3 _movementVector;
    private Vector3 _forward;
    private Vector3 _right;

    private Vector3 _velocity;
    private Quaternion _rotation;

    private float _targetAngle;

    public MoveState(Transform cameraTransform, Vector3EventChannel onMoveEvent, EventChannelSO onJumpEvent, float movementSpeed, float jumpSpeed)
    {
        _movementSpeed = movementSpeed;
        _jumpSpeed = jumpSpeed;
        _cameraTransform = cameraTransform;

        onMoveEvent.RegisterObserver(OnInputChanged);
        onJumpEvent.RegisterObserver(OnJump);
    }

    public (Vector3, Quaternion) GetVelocityAndRotation => (_velocity, _rotation);

    public void OnEnter()
    {
        _movementVector = new Vector3(_movementVector.x, 0, _movementVector.z);
    }

    public void OnExit()
    {

    }

    public void OnInputChanged(Vector3 moveVector)
    {
        _movementVector = moveVector;
    }

    public void OnJump()
    {
        _movementVector = new Vector3(_movementVector.x, _jumpSpeed, _movementVector.z);
    }

    public void Tick()
    {
        _forward = _cameraTransform.forward;
        _right = _cameraTransform.right;

        _forward.y = 0;
        _right.y = 0;

        _forward.Normalize();
        _right.Normalize();

        var moveDirection = (_movementVector.z * _forward + _movementVector.x * _right).normalized;
        if (moveDirection.magnitude < 0.1f) return;

        _velocity = moveDirection * _movementSpeed;

        if (_movementVector == Vector3.zero) return;

        _targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        _rotation = Quaternion.Euler(0, _targetAngle, 0);
    }
}
