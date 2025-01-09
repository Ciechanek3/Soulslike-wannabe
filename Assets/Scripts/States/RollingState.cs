using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : IState
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private float _speed;

    public RollingState(Transform transform, Rigidbody rb, float rollingSpeed)
    {
        _transform = transform;
        _rigidbody = rb;
        _speed = rollingSpeed;
        //damagable class
    }
    public void OnEnter()
    {
        //damagable set active false
    }

    public void OnExit()
    {
        //damagable set active true
    }

    public void Tick()
    {
        _rigidbody.velocity = _transform.right * _speed;
    }
}
