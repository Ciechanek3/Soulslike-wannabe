using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseStrategy : IStrategy
{
    private Transform _chaseTarget;
    private NavMeshAgent _agent;
    private float _movementSpeed;
    private float _range;

    public ChaseStrategy(Transform chaseTarget, NavMeshAgent agent, float ms, float range)
    {
        _chaseTarget = chaseTarget;
        _agent = agent;
        _movementSpeed = ms;
        _range = range;
    }

    public Node.Status Process()
    {
        _agent.SetDestination(_chaseTarget.position);
        _chaseTarget.transform.LookAt(_chaseTarget);

        if (_agent.remainingDistance < _range)
        {
            return Node.Status.Success;
        }

        return Node.Status.Running;
    }

    public void Reset()
    {
        _chaseTarget = null;
    }
}
public class AttackStrategy : IStrategy
{
    private Collider _weaponCollider;
    private float _range;
    private AnimatorController _animatorController;

    

    AttackStrategy(Collider weaponCollider, AnimatorController animatorController, float range)
    {
        _weaponCollider = weaponCollider;
        _animatorController = animatorController;
        _range = range;
    }

    public Node.Status Process()
    {
        if(_weaponCollider.enabled == false)
        {
            _weaponCollider.enabled = true;
        }
        else
        {
            if (_animatorController.IsAnimationFinished)
            {
                _weaponCollider.enabled = false;
                return Node.Status.Success;
            }
        }

        return Node.Status.Running;
    }

    public void Reset()
    {
        
    }
}
