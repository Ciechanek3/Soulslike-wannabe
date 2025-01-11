using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInTarget : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float range;

    private TargetLocator _targetLocator;
    private ILockable _currentTarget;

    public ILockable CurrentTarget => _currentTarget;

    private void Start()
    {
        _targetLocator = new TargetLocator(range);
    }

    public void FindClosestTarget(Transform playerTransform)
    {
        _targetLocator.TryFindLockableTarget(playerTransform, out _currentTarget);
    }
}
