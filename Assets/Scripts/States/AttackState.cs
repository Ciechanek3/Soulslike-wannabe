using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Rigidbody _rigidbody;
    public AttackState(Rigidbody rb)
    {
        _rigidbody = rb;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
