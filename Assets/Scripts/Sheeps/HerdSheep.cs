using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HerdSheep : MonoBehaviour
{
    [SerializeField]
    private Sheep _sheepPrefab;

    private List<Sheep> _herdSheepList;

    public static HerdSheep Instance;

    public Vector3 AveragePosition { get; private set; }
    public int SheepCount { get => _herdSheepList.Count; }
    public int AdultSheepCount { get; private set; }
    public float Offset { get; private set; }

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

        if (_herdSheepList == null)
            _herdSheepList = new List<Sheep>();
    }

    private void Start()
    {
        CreateFirstSheep();
    }

    private void FixedUpdate()
    {
        UpdateAveragePosition();
    }

    private void UpdateAveragePosition()
    {
        if (SheepCount > 0)
        {
            float averagePositionX = _herdSheepList.Average(x => x.transform.position.x);
            float averagePositionY = _herdSheepList.Average(y => y.transform.position.y);
            float averagePositionZ = _herdSheepList.Average(z => z.transform.position.z);

            AveragePosition = new Vector3(averagePositionX, averagePositionY, averagePositionZ);
        }
        else
        {
            AveragePosition = _sheepPrefab.transform.position;
        }
    }

    private void CalculateOffset()
    {
        if (SheepCount < 10)
        {
            Offset = 0.5f;
        }
        else if (SheepCount >= 10 && SheepCount < 30)
        {
            Offset = 1f;
        }
        else if (SheepCount >= 30 && SheepCount < 80)
        {
            Offset = 1.5f;
        }
        else if (SheepCount >= 80 && SheepCount < 130)
        {
            Offset = 2f;
        }
        else if (SheepCount >= 130 && SheepCount < 200)
        {
            Offset = 2.5f;
        }
        else if (SheepCount >= 200 && SheepCount < 300)
        {
            Offset = 3f;
        }
        else if (SheepCount >= 300 && SheepCount < 500)
        {
            Offset = 3.5f;
        }
        else if (SheepCount >= 500 && SheepCount < 700)
        {
            Offset = 4f;
        }
        else if (SheepCount >= 700 && SheepCount < 1000)
        {
            Offset = 4.5f;
        }
        else
        {
            Offset = 5f;
            //Offset = 0.5f * (SheepCount / 10);
        }
        //Debug.Log("Offset " + Offset);
    }

    private void CreateFirstSheep()
    {
        var sheep = Instantiate(_sheepPrefab, this.transform);
        sheep.Init(this, AveragePosition);
        _herdSheepList.Add(sheep);
        CalculateOffset();
    }

    public void RemoveSheep(Sheep sheep)
    {
        _herdSheepList.Remove(sheep);
    }

    public void CloneSheep(Sheep sheep)
    {
        //var sheepCopy = Instantiate(sheep, this.transform);
        var sheepCopy = Instantiate(sheep, sheep.transform.position, Quaternion.identity, this.transform);
        sheepCopy.Init(this, sheep.transform.position);
        _herdSheepList.Add(sheepCopy);
        CalculateOffset();
    }

    public void AddAdultSheep()
    {
        AdultSheepCount += 1;
    }
}
