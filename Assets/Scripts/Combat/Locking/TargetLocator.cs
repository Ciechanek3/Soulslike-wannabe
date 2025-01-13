using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator
{
    private float _range;
    private float _dotProductThreshold;

    private Enemy[] temp;

    public TargetLocator(float range)
    {
        _range = range;
    }

    public bool TryFindLockableTarget(Transform t, out ILockable lockable)
    {
        lockable = GetClosestEnemyInDirection(t);
        return lockable != null;
    }


    ILockable GetClosestEnemyInDirection(Transform t)
    {
        ILockable closestLockable = null;
        float maxDotProduct = _dotProductThreshold;

        temp = Object.FindObjectsOfType<Enemy>();
        foreach (ILockable lockable in temp)
        {
            Vector3 enemyDirection = (lockable.LockTransform.position - t.position).normalized;
            float dotProduct = Vector3.Dot(Vector3.forward, enemyDirection);

            if (dotProduct > maxDotProduct && (Vector3.Distance(t.position, lockable.LockTransform.position)) <= _range)
            {
                maxDotProduct = dotProduct;
                closestLockable = lockable;
            }
        }

        return closestLockable;
    }
}
