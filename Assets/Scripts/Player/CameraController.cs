using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Lock Detection Settings")]
    [SerializeField] private float range;

    [SerializeField] private Transform lookTarget;

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
        onLockEventChannel.RegisterObserver(UpdateRotateVector);
    }

    private void OnDisable()
    {
        onLockEventChannel.UnregisterObserver(UpdateRotateVector);
    }

    private void UpdateRotateVector(Vector3 vector)
    {
        _moveVector = vector;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(lookTarget.transform.position, Vector3.up, _moveVector.x * mouseSensitivity);
        transform.RotateAround(lookTarget.transform.position, transform.right, _moveVector.y * mouseSensitivity);
    }

    private void LockTarget(Vector3 cameraInput)
    {
        if (_targetLocator.TryFindLockableTarget(lookTarget.transform, out _currentTarget))
        {
            transform.LookAt(_currentTarget.LockTransform);
        }
    }
}
