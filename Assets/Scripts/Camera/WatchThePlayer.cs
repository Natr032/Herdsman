using UnityEngine;

public class WatchThePlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    private void Update()
    {
        transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y);
    }
}
