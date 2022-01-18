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
        //Считываем левый клик мыши, когда кнопка была отжата
        if (Input.GetMouseButtonUp(0) == true)
        {
            Vector2 mousePos = Input.mousePosition;
            //В mousePos находится экранное положение мыши
            //(относительно разрешения экрана)
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //В worldPos мы перевели положение 
            //мыши относительно нашей главной камеры
            //worldPos - та точка, которая нам и нужна
            _needToGo = true;
        }
        //если значение выставленно, то наш объект должен двигаться в нужную сторону
        if (_needToGo)
        {
            //_rigidBody2d.MovePosition(Vector2.MoveTowards(transform.position, worldPos, _speed * Time.deltaTime));
            transform.position = Vector3.MoveTowards(transform.position, worldPos, _speed * Time.deltaTime);
            //Плавное перемещение до точки
            //Умножаем на Time.timeScale для того, 
            //чтоб было плавнее и картинка не дергалась
            //Теперь проверяем расстояние до цели
            if (Vector2.Distance(transform.position, worldPos) < 0.01)
            {
                _needToGo = false;//Выключаем, если дошли
            }
        }
    }
}
