using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private Rigidbody _rigidbody;
    private Transform _cameraTransform;
    
    private Quaternion _targetRotation;
    private float _movementSpeed;
    private float _jumpSpeed;

    private Vector3 _movementVector;
    private Vector3 _moveInput;
    private float _targetAngle;

    public MoveState(Rigidbody rb, Transform cameraTransform, Vector3EventChannel onMoveEvent, EventChannelSO onJumpEvent, float movementSpeed, float jumpSpeed)
    {
        _rigidbody = rb;
        _movementSpeed = movementSpeed;
        _jumpSpeed = jumpSpeed;
        _cameraTransform = cameraTransform;
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
        _moveInput = moveVector;
        HandleRotation();
    }

    public void OnJump()
    {
        _movementVector = new Vector3(_movementVector.x, _jumpSpeed, _movementVector.z);
    }

    public void Tick()
    {
        _targetAngle = Mathf.Atan2(-_moveInput.x, _moveInput.z) * _movementSpeed * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
        _movementVector = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;

        _targetRotation = Quaternion.Euler(0f, Quaternion.LookRotation(_movementVector.normalized).eulerAngles.y - 90, 0f);

        _rigidbody.velocity = _movementVector;
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, _targetRotation, 10f * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        
        
    }
}
