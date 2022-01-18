using UnityEngine;
using UnityEngine.Events;

public abstract class DroppedBase : MonoBehaviour
{
    public UnityEvent OnDropedEvent;
    public UnityEvent OnTimeIsRunningOutedEvent;
    public UnityEvent OnDestroyedEvent;
    public UnityEvent OnPickedUpEvent;
}
