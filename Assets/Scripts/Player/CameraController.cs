using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform lookTarget;
    [SerializeField] private CinemachineFreeLook mainCamera;
    [SerializeField] private CinemachineVirtualCamera lockCamera;

    [Header("Lock Detection Settings")]
    [SerializeField] private LayerMask layerToFind;
    [SerializeField] private float range;
    [SerializeField] private float angle;
    [SerializeField] private float dotProductThreshold;
        
    [SerializeField] private EventChannelSO onLockEventChannel;

    private TargetLocator _lockTargetLocator;
    private ILockable _currentTarget;

    public ILockable CurrentTraget => _currentTarget;

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
        if(mainCamera.enabled)
        {
            mainCamera.enabled = false;
            lockCamera.enabled = true;
        }
        else
        {
            lockCamera.enabled = false;
            mainCamera.enabled = true;
        }

        if (_lockTargetLocator.TryFindLockableTarget(out _currentTarget))
        {
            lockCamera.LookAt = _currentTarget.LockTransform;
        }
    }
}
