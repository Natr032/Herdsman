using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _interpolation = 0.5f;
    [SerializeField]
    private float _waitTime;

    private Sheep _sheep;
    private HerdSheep _herdSheep;
    private Player _player;

    private void Awake()
    {
        transform.TryGetComponent<Sheep>(out _sheep);
        _player = Player.Instance;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Движение в сторону стада
        if (_herdSheep != null)
        {
            if (transform.position.x > _herdSheep.AveragePosition.x + _herdSheep.Offset)
            {
                transform.position = GoToTarget(_herdSheep.AveragePosition);
            }
            else if (transform.position.y > _herdSheep.AveragePosition.y + _herdSheep.Offset)
            {
                transform.position = GoToTarget(_herdSheep.AveragePosition);
            }
            else if (transform.position.x < _herdSheep.AveragePosition.x - _herdSheep.Offset)
            {
                transform.position = GoToTarget(_herdSheep.AveragePosition);
            }
            else if (transform.position.y < _herdSheep.AveragePosition.y - _herdSheep.Offset)
            {
                transform.position = GoToTarget(_herdSheep.AveragePosition);
            }
            else
            {
                _animator.SetBool("Move", false);
            }
        }
        else
        {
            _herdSheep = _sheep.HerdSheep;
        }

        // Движение в сторону игрока
        if (_player != null)
        {
            if (transform.position.x > _player.gameObject.transform.position.x + _herdSheep.Offset)
            {
                transform.position = GoToTarget(_player.gameObject.transform.position);
            }
            else if (transform.position.y > _player.gameObject.transform.position.y + _herdSheep.Offset)
            {
                transform.position = GoToTarget(_player.gameObject.transform.position);
            }
            else if (transform.position.x < _player.gameObject.transform.position.x - _herdSheep.Offset)
            {
                transform.position = GoToTarget(_player.gameObject.transform.position);
            }
            else if (transform.position.y < _player.gameObject.transform.position.y - _herdSheep.Offset)
            {
                transform.position = GoToTarget(_player.gameObject.transform.position);
            }
            else
            {
                _animator.SetBool("Move", false);
            }
        }
        else
        {
            _player = Player.Instance;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }

    private Vector3 GoToTarget(Vector3 targetPos)
    {
        _animator.SetBool("Move", true);
        return Vector3.MoveTowards(transform.position, targetPos, _interpolation * _speed * Time.deltaTime);
    }
}
