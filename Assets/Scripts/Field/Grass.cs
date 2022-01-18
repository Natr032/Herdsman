using System.Collections;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;
    [SerializeField]
    private Collider2D _collider;
    [SerializeField, Range(1, 1000)]
    private float _maxFood;
    [SerializeField]
    private float _oneServing;
    [SerializeField]
    private float _delay;
    [SerializeField]
    private ContactFilter2D _colliderFilter;

    private float _food;
    private WaitForSeconds _waitEat;
    private WaitForSeconds _waitRecovery;
    private Color _color;

    public float Food 
    {   
        get => _food;
        private set => _food = (_food - value) < 0 ? 0 : _food - value;
    }
    public bool IsEated { get; private set; }
    public bool IsRecovery { get; private set; }

    private void Awake()
    {
        _waitEat = new WaitForSeconds(_delay);
        _waitRecovery = new WaitForSeconds(_delay * 3);
        _food = _maxFood;
        IsEated = false;
        IsRecovery = false;
    }

    private void Update()
    {
        if (Food == 0 && !IsRecovery)
        {
            _collider.enabled = false;
            StopAllCoroutines();
            StartCoroutine(RecoveryGrass());
        }

        if (Food == _maxFood)
        {
            _collider.enabled = true;
        }

        if (!_collider.IsTouching(_colliderFilter) && IsEated)
        {
            IsEated = false;
            StopAllCoroutines();
            StartCoroutine(RecoveryGrass());
        }
    }

    private void Eat(float food)
    {
        Food = food;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Sheep>())
        {
            if (!IsEated)
            {
                StopCoroutine(RecoveryGrass());
                StartCoroutine(EatGrass());
            }
        }
    }

    private IEnumerator EatGrass()
    {
        IsEated = true;
        while (Food > 0)
        {
            yield return _waitEat;
            Eat(_oneServing);

            // ВизуальныЙ эффект для теста
            _color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _sprite.color.a - 0.01f);
            _sprite.color = _color;
        }
        IsEated = false;
    }

    private IEnumerator RecoveryGrass()
    {
        IsRecovery = true;
        while (Food < _maxFood)
        {
            yield return _waitRecovery;
            _food += _oneServing;

            // ВизуальныЙ эффект для теста
            _color = new Color(_sprite.color.r, _sprite.color.g, _color.b, _sprite.color.a + 0.01f);
            _sprite.color = _color;
        }
        IsRecovery = false;
    }
}
