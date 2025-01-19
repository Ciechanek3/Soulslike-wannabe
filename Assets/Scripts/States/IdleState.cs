using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState, IMovementModel
{
    private Vector3 _movementVector;
    private float _jumpSpeed;

    public IdleState(EventChannelSO onJumpEvent, float jumpSpeed)
    {
        _jumpSpeed = jumpSpeed;
        onJumpEvent.RegisterObserver(OnJump);
    }

    public (Vector3, Quaternion) GetVelocityAndRotation => (_movementVector, Quaternion.identity);

    public void OnEnter()
    {
        _movementVector = Vector3.zero;
    }

    public void OnExit()
    {

    }

    public void OnJump()
    {
        _movementVector = new Vector3(0, _jumpSpeed, 0);
    }

    public void Tick()
    {

    }
}
