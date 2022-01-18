using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IDropable
{
    [SerializeField, Range(0, 100)]
    private int _percentProbability;
    [SerializeField]
    private List<DroppedBase> _listPrefabsLoot;

    private void ChoiceDropItem()
    {
        int chance = Random.Range(1, 101);
#if UNITY_EDITOR
        Debug.Log(chance);
#endif
        if (_listPrefabsLoot != null && chance <= _percentProbability)
        {
            int item = Random.Range(0, _listPrefabsLoot.Count);
            var loot = Instantiate(_listPrefabsLoot[item]);
            loot.transform.position = transform.position;
        }
    }

    public void Drop()
    {
        ChoiceDropItem();
    }
}
