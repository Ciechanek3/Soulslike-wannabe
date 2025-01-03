using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private Rigidbody _rigidbody;
    private Vector3 _movementVector;
    private float _movementSpeed;
    private float _jumpSpeedMultiplier;
    
    public MoveState(Rigidbody rb, Vector3EventChannel onMoveEvent, EventChannelSO onJumpEvent, float movementSpeed, float jumpSpeedMultiplier)
    {
        _rigidbody = rb;
        _movementSpeed = movementSpeed;
        _jumpSpeedMultiplier = jumpSpeedMultiplier;
        onMoveEvent.RegisterObserver(OnInputChanged);
        onJumpEvent.RegisterObserver(OnJump);
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnInputChanged(Vector3 moveVector)
    {
        _movementVector = new Vector3(moveVector.x, _movementVector.y, moveVector.z); ;
    }

    public void OnJump()
    {
        _movementVector = new Vector3(_movementVector.x, _jumpSpeedMultiplier, _movementVector.z); ;
    }

    public void Tick()
    {
        _rigidbody.velocity = _movementVector * _movementSpeed * Time.deltaTime;
    }
}
