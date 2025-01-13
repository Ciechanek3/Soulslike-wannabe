using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Lock Detection Settings")]
    [SerializeField] private float range;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private Vector3EventChannel onLockEventChannel;

    private TargetLocator _targetLocator;
    private ILockable _currentTarget;
    private Vector3 _moveVector;

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

    private void UpdateRotateVector(Vector3 vector)
    {
        _moveVector = vector;
    }

    private void Update()
    {
        
    }

    private void LockTarget(Vector3 cameraInput)
    {
        if (_targetLocator.TryFindLockableTarget(playerController.transform, out _currentTarget))
        {
            transform.LookAt(_currentTarget.LockTransform);
        }
    }
}
