using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : IState
{
    public RollingState()
    {
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
        
    }
}
