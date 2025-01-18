using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private Rigidbody _rigidbody;
    private Transform _cameraTransform;

    private float _movementSpeed;
    private float _rotationSpeed;
    private float _jumpSpeed;

    private Vector3 _movementVector;

    private float _targetAngle;

    public MoveState(Rigidbody rb, Transform cameraTransform, Vector3EventChannel onMoveEvent, EventChannelSO onJumpEvent, float movementSpeed, float jumpSpeed, float rotationSpeed)
    {
        _rigidbody = rb;
        _movementSpeed = movementSpeed;
        _jumpSpeed = jumpSpeed;
        _cameraTransform = cameraTransform;
        _rotationSpeed = rotationSpeed;
        onMoveEvent.RegisterObserver(OnInputChanged);
        onJumpEvent.RegisterObserver(OnJump);
    }

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
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        var moveDirection = (_movementVector.z * forward + _movementVector.x * right).normalized;
        if (moveDirection.magnitude < 0.1f) return;

        _rigidbody.velocity = moveDirection * _movementSpeed;

        if (_movementVector == Vector3.zero) return;

        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

    }
}
