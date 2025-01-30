using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour, IEnemy, ILockable, IDamagable
{
    [SerializeField] protected Animator _animator;
    
    protected NavMeshAgent _navMeshAgent;
    protected AnimatorController _animatorController;
    protected BehaviourTree _behaviourTree;
    

    public Transform LockTransform => transform;

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animatorController = new AnimatorController(_animator);
        _behaviourTree = new BehaviourTree();
    }

    public void GetHit(int damage)
    {
        throw new System.NotImplementedException();
    }
}
