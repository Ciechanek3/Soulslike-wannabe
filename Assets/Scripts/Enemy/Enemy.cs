using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, ILockable, IDamagable
{
    [SerializeField] private Animator _animator;

    private AnimatorController _animatorController;
    private BehaviourTree _behaviourTree;

    public Transform LockTransform => transform;

    private void Awake()
    {
        _animatorController = new AnimatorController(_animator);

        _behaviourTree = new BehaviourTree();
    }

    public void GetHit(int damage)
    {
        throw new System.NotImplementedException();
    }
}
