using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator
{
    private Transform _transform;
    private LayerMask _layerToSearch;

    private float _range;
    private float _angle;
    private float _dotProductThreshold;

    private List<ILockable> targets = new List<ILockable>();

    public TargetLocator(Transform t, LayerMask layerToSearch, float range, float angle, float dotProductThreshold)
    {
        _transform = t;
        _layerToSearch = layerToSearch;
        _range = range;
        _angle = angle;
        _dotProductThreshold = dotProductThreshold;
    }

    public void FindAllTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _range, _layerToSearch);

        foreach (Collider collider in colliders)
        {
            Vector3 directionToTarget = (collider.transform.position - _transform.position).normalized;
            float angleToTarget = Vector3.Angle(_transform.forward, directionToTarget);

            if (angleToTarget < _angle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(_transform.position, directionToTarget, out hit, _range))
                {
                    if (hit.collider == collider)
                    {
                        if(collider.TryGetComponent<ILockable>(out var lockable))
                        {
                            targets.Add(lockable);
                        } 
                    }
                }
            }
        }
    }

    public bool TryFindLockableTarget(out ILockable lockable)
    {
        FindAllTargets();
        lockable = GetClosestEnemyInDirection();
        return lockable != null;
    }


    ILockable GetClosestEnemyInDirection()
    {
        ILockable closestLockable = null;
        float maxDotProduct = _dotProductThreshold;

        foreach (ILockable lockable in targets)
        {
            Vector3 enemyDirection = (lockable.LockTransform.position - _transform.position).normalized;
            float dotProduct = Vector3.Dot(Vector3.forward, enemyDirection);

            if (dotProduct > maxDotProduct && (Vector3.Distance(_transform.position, lockable.LockTransform.position)) <= _range)
            {
                maxDotProduct = dotProduct;
                closestLockable = lockable;
            }
        }

        return closestLockable;
    }
}
