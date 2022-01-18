using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : DroppedBase
{
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private List<Weapon> _listPrefabsWeapon;

    private int _playerWeaponLvl;
    private int _nextWeaponLvl;
    private bool _isBlinking = false;

    private void Awake()
    {
        SortWeapon();
        ChoiceWeapon();
        _renderer.sprite = _listPrefabsWeapon[_nextWeaponLvl].GetComponent<Weapon>().IconWeapon;
    }

    private void Start()
    {
        OnDropedEvent?.Invoke(); // Добавить анимацию падения при появлении
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0 || Player.Instance.Weapon.WeaponLevel >= _nextWeaponLvl)
        {
            OnDestroyedEvent?.Invoke(); // Добавить анимацию исчезновения
            Destroy(gameObject);
        }
        
        if (_lifeTime < 5 && !_isBlinking)
        {
            OnTimeIsRunningOutedEvent?.Invoke(); // Добавить анимацию мигания
            _isBlinking = true;
        }
    }

    private void ChoiceWeapon()
    {
        if (Player.Instance)
        {
            _playerWeaponLvl = Player.Instance.Weapon.WeaponLevel;
        }

        var nextLvl = _playerWeaponLvl + 1;
        foreach (var element in _listPrefabsWeapon)
        {
            if (nextLvl == element.GetComponent<Weapon>().WeaponLevel)
            {
                _nextWeaponLvl = nextLvl;
                return;
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            OnPickedUpEvent?.Invoke(); // Добавить анимацию при подборе игроком
            Destroy(player.Weapon.gameObject);
            player.Weapon = Instantiate(_listPrefabsWeapon[_nextWeaponLvl], player.transform);
            Destroy(gameObject);
        }
    }

    private void SortWeapon()
    {
        Weapon item = _listPrefabsWeapon[0];
        for (int i = 0; i < _listPrefabsWeapon.Count; i++)
        {
            for (int j = i + 1; j < _listPrefabsWeapon.Count; j++)
            {
                if (_listPrefabsWeapon[i].GetComponent<Weapon>().WeaponLevel > _listPrefabsWeapon[j].GetComponent<Weapon>().WeaponLevel)
                {
                    item = _listPrefabsWeapon[i];
                    _listPrefabsWeapon[i] = _listPrefabsWeapon[j];
                    _listPrefabsWeapon[j] = item;
                }
            }
        }
    }
}
