using UnityEngine;
using System.Collections;

public class SheepEating : MonoBehaviour
{
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private Sheep _sheep;
    [SerializeField]
    private float _delay;
    [SerializeField]
    private float _step;
    [SerializeField]
    private float _maxWeight;
    [SerializeField]
    private ContactFilter2D _grassFilter;

    public float Weight { get; private set; }
    public bool IsGainWeight { get; private set; }

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        IsGainWeight = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // если овца находится в зоне питания, тогда она ест
        if (collision.TryGetComponent(out Grass grass))
        {
            if (grass.IsEated)
            {
                if (Weight < _maxWeight && !IsGainWeight)
                {
                    IsGainWeight = true;
                    StartCoroutine(GainWeight());
#if UNITY_EDITOR
                    //Debug.Log("Start GainWeight");
#endif
                }
            }
        }
    }

    private IEnumerator GainWeight()
    {
        while (Weight < _maxWeight)
        {
            yield return _wait;
            if (!_collider.IsTouching(_grassFilter))
            {
#if UNITY_EDITOR
                //Debug.Log("Not grass");
#endif
                IsGainWeight = false;
                yield break;
            }
            Weight += _step;
#if UNITY_EDITOR
            //Debug.Log("Weight " + Weight);
#endif
        }
        IsGainWeight = false;
#if UNITY_EDITOR
        //Debug.Log("Weight false");
#endif
        _sheep.Increase();
        StartCoroutine(MakeCopy());
    }

    private IEnumerator MakeCopy()
    {
        float temp = 0;
        float randNum = Random.Range(_maxWeight / 1.4f, _maxWeight);
        while (temp < randNum)
        {
            yield return _wait;
            temp += _step;
#if UNITY_EDITOR
            //Debug.Log(temp);
#endif
        }
        _sheep.Clone();
        StartCoroutine(MakeCopy());
    }
}
