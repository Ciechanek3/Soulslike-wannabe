using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform lookTarget;
    [SerializeField] private CinemachineFreeLook cinemachine;

    [Header("Lock Detection Settings")]
    [SerializeField] private LayerMask layerToFind;
    [SerializeField] private float range;
    [SerializeField] private float angle;
    [SerializeField] private float dotProductThreshold;

    [SerializeField] private EventChannelSO onLockEventChannel;

    private TargetLocator _lockTargetLocator;
    private ILockable _currentTarget;

    private void Awake()
    {
        _lockTargetLocator = new TargetLocator(lookTarget, layerToFind, range, angle, dotProductThreshold);
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
        if (_lockTargetLocator.TryFindLockableTarget(out _currentTarget))
        {
            cinemachine.LookAt = _currentTarget.LockTransform;
        }
    }
}
