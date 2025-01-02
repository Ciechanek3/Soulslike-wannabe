using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private Vector3 _movementVector;
    private float _movementSpeed;
    private Transform _transform;
    public MoveState(Transform transform, Vector3EventChannel onMoveEvent, float movementSpeed)
    {
        _transform = transform;
        _movementSpeed = movementSpeed;
        onMoveEvent.RegisterObserver(OnInputChanged);
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnInputChanged(Vector3 moveVector)
    {
        _movementVector = moveVector;
    }

    public void Tick()
    {
        _transform.Translate(_movementVector * _movementSpeed * Time.deltaTime);
    }
}
