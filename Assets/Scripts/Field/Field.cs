using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private Grass[] _prefabGround;
    private Grass[,] _groundHolder;

    private void Awake()
    {
        if (_prefabGround != null)
        {
            CreateGrassland();
        }
    }

    private void CreateGrassland()
    {
        int fieldWidth = (int)(transform.localScale.x / 10);
        int fieldHeight = (int)(transform.localScale.y / 10);
        _groundHolder = new Grass[fieldWidth, fieldHeight];

        var startPosition = new Vector2(-(transform.localScale.x / 2) + 5f, (transform.localScale.y / 2) - 5f);
        var position = startPosition;
        for (int i = 0; i < _groundHolder.GetLength(0); i++)
        {
            for (int j = 0; j < _groundHolder.GetLength(1); j++)
            {
                var ground = Instantiate(_prefabGround[Random.Range(0, _prefabGround.Length - 1)], transform.position, Quaternion.identity, this.transform);
                ground.transform.position = new Vector3(position.x, position.y);
                ground.name = "Grass " + i + j;
                position.x += 10f;
                _groundHolder[i, j] = ground;
            }
            position.x = startPosition.x;
            position.y -= 10f;
        }
    }
}
