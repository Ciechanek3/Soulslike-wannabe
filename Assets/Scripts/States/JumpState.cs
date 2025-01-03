using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState, IInputObserver
{
    private Vector3 _movementVector;
    public JumpState(Rigidbody rb)
    {

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

    }
}
