using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform lookTarget;
    [SerializeField] private CinemachineBrain cinemachineBrain;
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
    public Action OnCameraChanged;
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
            if (mainCamera.gameObject.activeInHierarchy)
            {
                if(_lockTargetLocator.TryFindLockableTarget(out _currentTarget))
                {
                    mainCamera.gameObject.SetActive(false);
                    lockCamera.gameObject.SetActive(true);
                StopCoroutine(WaitForBlendToFinish(cinemachineBrain.m_DefaultBlend.BlendTime));
                OnCameraChanged?.Invoke();
            } 
            }
            else
            {
                lockCamera.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
                _currentTarget = null;
            StartCoroutine(WaitForBlendToFinish(cinemachineBrain.m_DefaultBlend.BlendTime));
        }
        
    }

    private IEnumerator WaitForBlendToFinish(float blendTime)
    {
        yield return new WaitForSeconds(blendTime);
        OnCameraChanged?.Invoke();
    }
}
