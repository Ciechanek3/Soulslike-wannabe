using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator
{
    private float _range;

    public TargetLocator(float range)
    {
        _range = range;
    }

    public bool TryFindLockableTarget(Transform t, out ILockable lockable)
    {
        lockable = null;
        if(Physics.Raycast(t.position, t.TransformDirection(Vector3.forward), out RaycastHit hit, _range))
        {
            if (hit.collider.TryGetComponent(out ILockable _lockable))
            {
                lockable = _lockable;
                return true;
            }
            return false;
        }
        return false;
    }
}
