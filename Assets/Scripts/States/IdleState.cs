using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Rigidbody _rigidbody;
    private Vector3 _movementVector;
    private float _jumpSpeed;

    public IdleState(Rigidbody rb, EventChannelSO onJumpEvent, float jumpSpeed)
    {
        _rigidbody = rb;
        _jumpSpeed = jumpSpeed;
        onJumpEvent.RegisterObserver(OnJump);
    }

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
        _rigidbody.velocity = _movementVector * Time.deltaTime;
    }
}
