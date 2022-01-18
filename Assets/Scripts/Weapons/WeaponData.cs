using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Weapon", order = 51)]
public class WeaponData : ScriptableObject
{
    [SerializeField, Range(0, 100)]
    public int _weaponLevel;
    public float _attackRadius;
    public Sprite _iconWeapon;
    public LayerMask _whatIsEnemies;
}
