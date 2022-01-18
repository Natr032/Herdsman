using UnityEngine;
using UnityEngine.Events;

public class Wolf : MonoBehaviour, IDeadeable
{
    public UnityEvent OnKilledEvent;

    public void Die()
    {
        OnKilledEvent?.Invoke(); // Добавить анимацию и эффекты при смерти
#if UNITY_EDITOR
        //Debug.Log("Dead " + this.name);
#endif
        Destroy(gameObject);
    }
}
