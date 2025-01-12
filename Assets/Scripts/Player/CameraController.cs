using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Lock Detection Settings")]
    [SerializeField] private float range;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private EventChannelSO onLockEventChannel;

    private TargetLocator _targetLocator;
    private ILockable _currentTarget;

    private void Awake()
    {
        _targetLocator = new TargetLocator(range);
    }

    private void OnEnable()
    {
        onLockEventChannel.RegisterObserver(LockTarget);
    }

    private void OnDisable()
    {
        onLockEventChannel.UnregisterObserver(LockTarget);
    }

    private void LockTarget()
    {
        if (_targetLocator.TryFindLockableTarget(playerController.transform, out _currentTarget))
        {
            transform.LookAt(_currentTarget.LockTransform);
        }
    }
}
