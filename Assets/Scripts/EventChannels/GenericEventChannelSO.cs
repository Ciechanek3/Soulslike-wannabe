using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannelSO<T> : ScriptableObject
{
    private UnityAction<T> OnEventRaised;

    public void RaiseEvent(T param)
    {
        OnEventRaised?.Invoke(param);
    }

    public void RegisterObserver(UnityAction<T> observer)
    {
        OnEventRaised += observer;
    }

    public void UnregisterObserver(UnityAction<T> observer)
    {
        OnEventRaised -= observer;
    }
}
