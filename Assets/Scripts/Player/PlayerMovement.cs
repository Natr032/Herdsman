using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody2d;
    [SerializeField]
    private float _speed = 0.05f;

    private bool _needToGo = false;
    private Vector2 worldPos;

    private void Update()
    {
        //��������� ����� ���� ����, ����� ������ ���� ������
        if (Input.GetMouseButtonUp(0) == true)
        {
            Vector2 mousePos = Input.mousePosition;
            //� mousePos ��������� �������� ��������� ����
            //(������������ ���������� ������)
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //� worldPos �� �������� ��������� 
            //���� ������������ ����� ������� ������
            //worldPos - �� �����, ������� ��� � �����
            _needToGo = true;
        }
        //���� �������� �����������, �� ��� ������ ������ ��������� � ������ �������
        if (_needToGo)
        {
            //_rigidBody2d.MovePosition(Vector2.MoveTowards(transform.position, worldPos, _speed * Time.deltaTime));
            transform.position = Vector3.MoveTowards(transform.position, worldPos, _speed * Time.deltaTime);
            //������� ����������� �� �����
            //�������� �� Time.timeScale ��� ����, 
            //���� ���� ������� � �������� �� ���������
            //������ ��������� ���������� �� ����
            if (Vector2.Distance(transform.position, worldPos) < 0.01)
            {
                _needToGo = false;//���������, ���� �����
            }
        }
    }
}
