using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    private WeaponData _weaponData;
    [SerializeField]
    public Transform _attackPoint;

    private const int MAX_WEAPON_LVL = 100;

    public int WeaponLevel 
    { 
        get => _weaponData._weaponLevel; 
        private set => _weaponData._weaponLevel = Mathf.Clamp(value, 0, MAX_WEAPON_LVL);
    }
    public Sprite IconWeapon { get => _weaponData._iconWeapon; }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _weaponData._attackRadius, _weaponData._whatIsEnemies);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Transform target = colliders[i].transform;
                if (target.TryGetComponent(out IDeadeable dead))
                {
                    dead.Die();
                }
            }
        }
    }
}
