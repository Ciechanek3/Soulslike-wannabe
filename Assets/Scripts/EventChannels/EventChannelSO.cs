using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event Channel", menuName = "Events/Event Channel")]
public class EventChannelSO : ScriptableObject
{
    private UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }

    public void RegisterObserver(UnityAction observer)
    {
        OnEventRaised += observer;
    }

    public void UnregisterObserver(UnityAction observer)
    {
        OnEventRaised -= observer;
    }
}
