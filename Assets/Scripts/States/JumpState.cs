using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState, IInputObserver
{
    private Vector3 _movementVector;
    public JumpState(Transform transform)
    {

    }
    public void OnEnter()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
