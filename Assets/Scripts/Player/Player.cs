using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;

    public static Player Instance;

    public Weapon Weapon { get => _weapon; set => _weapon = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out Weapon weapon))
    //    {
    //        if (weapon.WeaponLevel > _weapon.WeaponLevel)
    //        {
    //            _weapon = weapon;
    //        }
    //    }
    //}
}
