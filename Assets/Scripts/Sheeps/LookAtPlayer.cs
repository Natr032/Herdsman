using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private bool _active;

    private Transform _target;
    private Vector3 _targetPos;
    private float _angle;

    private bool _facingRight = true;

    private void Start()
    {
        _target = Player.Instance.transform;
    }

    private void LateUpdate()
    {
        if (_active)
        {
            _targetPos = _target.position;
            _targetPos.x = _targetPos.x - transform.position.x;
            _targetPos.y = _targetPos.y - transform.position.y;
            _angle = Mathf.Atan2(_targetPos.y, _targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        }
        else
        {
            _targetPos = _target.position;
            if (transform.position.x > _targetPos.x)
            {
                if (_facingRight)
                {
                    Flip();
                }
            }
            else if (transform.position.x < _targetPos.x)
            {
                if (!_facingRight)
                {
                    Flip();
                }
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
