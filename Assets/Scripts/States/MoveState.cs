using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private Rigidbody _rigidbody;
    private Vector3 _movementVector;
    private Quaternion _targetRotation;
    private float _movementSpeed;
    private float _jumpSpeed;
    
    public MoveState(Rigidbody rb, Vector3EventChannel onMoveEvent, EventChannelSO onJumpEvent, float movementSpeed, float jumpSpeed)
    {
        _rigidbody = rb;
        _movementSpeed = movementSpeed;
        _jumpSpeed = jumpSpeed;
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
        _movementVector = new Vector3(moveVector.x * _movementSpeed, _movementVector.y, moveVector.z * _movementSpeed);
        HandleRotation();
    }

    public void OnJump()
    {
        _movementVector = new Vector3(_movementVector.x, _jumpSpeed, _movementVector.z);
    }

    public void Tick()
    {
        _rigidbody.velocity = _movementVector;
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, _targetRotation, 10f * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        if (_movementVector == Vector3.zero) return;
        _targetRotation = Quaternion.Euler(0f, Quaternion.LookRotation(_movementVector.normalized).eulerAngles.y - 90, 0f);
        
    }
}
