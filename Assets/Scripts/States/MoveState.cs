using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState, IInputObserver
{
    private Vector3 _movementVector;
    private float _movementSpeed;
    private Transform _transform;
    public MoveState(Transform transform, float movementSpeed)
    {
        _transform = transform;
        _movementSpeed = movementSpeed;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnInputChanged(Vector3 moveVector)
    {
        _movementVector = moveVector;
    }
    public void Tick()
    {
        
    }
}
