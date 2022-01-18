using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    [SerializeField]
    private Wolf _wolf;

    private float _speed;
    private HerdSheep _herdSheep;
    private Vector3 _startPosition;
    private bool _grabbedSheep;
    private bool _facingRight = false;

    public void Init(HerdSheep herd, float speed)
    {
        _herdSheep = herd;
        _speed = speed;
    }

    private void Awake()
    {
        _grabbedSheep = false;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!_grabbedSheep)
        {
            if (_herdSheep)
            {
                Move(_herdSheep.AveragePosition);
            }
        }
        else
        {
            if (transform.position != _startPosition)
            {
                Move(_startPosition);
            }
            else
            {
                _wolf.Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Sheep sheep))
        {
            if (!_grabbedSheep)
            {
                _grabbedSheep = true;
                sheep.Die();
            }
        }
    }

    private void Move(Vector3 targetPosition)
    {
        Vector3 newPosition;
        newPosition = new Vector3(targetPosition.x, targetPosition.y, 1);
        //_animator.SetBool("Move", true);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.deltaTime);

        if (transform.position.x > targetPosition.x)
        {
            if (_facingRight)
            {
                Flip();
            }
        }
        else if (transform.position.x < targetPosition.x)
        {
            if (!_facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
