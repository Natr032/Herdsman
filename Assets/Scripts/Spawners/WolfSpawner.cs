using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    [SerializeField]
    private Field _field;
    [SerializeField]
    private GameObject _prefabWolf;
    [SerializeField]
    private float _spawnerTimeMin;
    [SerializeField]
    private float _spawnerTimeMax;

    private float _timeLeft;
    private int _height;
    private int _width;

    private void Awake()
    {
        _height = (int) _field.transform.localScale.y;
        _width = (int) _field.transform.localScale.x;
    }

    private void Start()
    {
        _timeLeft = Random.Range(_spawnerTimeMin, _spawnerTimeMax);
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0)
        {
            if (Time.time <= 120f)
            {
                SpawnWolf(2f);
                _timeLeft = Random.Range(30f, 60f);
#if UNITY_EDITOR
                Debug.Log("1 интервал: создание врага");
#endif
            }
            else if (Time.time > 120f && Time.time <= 180f)
            {
                SpawnWolf(2.3f);
                _timeLeft = Random.Range(30f, 50f);
#if UNITY_EDITOR
                Debug.Log("2 интервал: создание врага");
#endif
            }
            else if (Time.time > 180f && Time.time <= 240f)
            {
                SpawnWolf(2.6f);
                _timeLeft = Random.Range(20f, 40f);
#if UNITY_EDITOR
                Debug.Log("3 интервал: создание врага");
#endif
            }
            else if (Time.time > 240f && Time.time <= 300f)
            {
                SpawnWolf(2.8f);
                _timeLeft = Random.Range(20f, 30f);
#if UNITY_EDITOR
                Debug.Log("4 интервал: создание врага");
#endif
            }
            else if (Time.time > 300f && Time.time <= 360f)
            {
                SpawnWolf(3f);
                _timeLeft = Random.Range(10f, 20f);
#if UNITY_EDITOR
                Debug.Log("5 интервал: создание врага");
#endif
            }
            else if (Time.time > 360f && Time.time <= 420f)
            {
                SpawnWolf(3.2f);
                _timeLeft = Random.Range(5f, 10f);
#if UNITY_EDITOR
                Debug.Log("6 интервал: создание врага");
#endif
            }
            else
            {
                SpawnWolf(3.5f);
                _timeLeft = Random.Range(1f, 5f);
#if UNITY_EDITOR
                Debug.Log("7 интервал: создание врага");
#endif
            }
        }
    }

    private void SpawnWolf(float wolfSpeed)
    {
        int randomY = 0;
        int randomX = 0;
        int rnd = Random.Range(0, 2);
        if (rnd > 0)
        {
            randomX = Random.Range(-(_width / 2), (_width / 2));
            if (randomX != -(_width / 2) || randomX != (_width / 2))
            {
                randomY = (Random.Range(0, 1f) >= 0.5f) ? (_height / 2) : -(_height / 2);
            }
            else
            {
                randomY = Random.Range(-(_height / 2), (_height / 2));
            }
        }
        else
        {
            randomY = Random.Range(-(_height / 2), (_height / 2));
            if (randomY != -(_height / 2) || randomY != (_height / 2))
            {
                randomX = (Random.Range(0, 1f) >= 0.5f) ? (_width / 2) : -(_width / 2);
            }
            else
            {
                randomX = Random.Range(-(_width / 2), (_width / 2));
            }
        }
        
        Vector3 spawnPosition = new Vector3(randomX, randomY, 1);
        var wolf = Instantiate(_prefabWolf, this.transform);
        wolf.transform.position = spawnPosition;
        wolf.TryGetComponent(out WolfMovement wolfMove);
        wolfMove?.Init(HerdSheep.Instance, wolfSpeed);
    }
}
