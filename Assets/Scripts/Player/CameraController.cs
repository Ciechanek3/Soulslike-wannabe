using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float smoothTime;

    [Header("Lock Detection Settings")]
    [SerializeField] private float range;

    [SerializeField] private Transform lookTarget;

    [SerializeField] private Vector3EventChannel onLockEventChannel;

    private TargetLocator _lockTargetLocator;
    private ILockable _currentTarget;
    private Vector3 _moveVector;
    private Vector3 _offset;
    private Vector3 _currentVelocity;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _lockTargetLocator = new TargetLocator(range);
        _offset = transform.position - lookTarget.position;
    }

    private void OnEnable()
    {
        onLockEventChannel.RegisterObserver(UpdateRotateVector);
    }

    private void OnDisable()
    {
        onLockEventChannel.UnregisterObserver(UpdateRotateVector);
    }

    private void UpdateRotateVector(Vector3 vector)
    {
        _moveVector = vector * mouseSensitivity;
    }

    private void LateUpdate()
    {
    }

    private void LockTarget(Vector3 cameraInput)
    {
        if (_lockTargetLocator.TryFindLockableTarget(lookTarget.transform, out _currentTarget))
        {
            transform.LookAt(_currentTarget.LockTransform);
        }
    }
}
