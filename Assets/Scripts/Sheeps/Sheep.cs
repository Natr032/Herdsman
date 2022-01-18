using UnityEngine;

public class Sheep : MonoBehaviour, IDeadeable
{
    [SerializeField]
    private Collider2D _collider;

    public HerdSheep HerdSheep { get; private set; }

    public void Init(HerdSheep herdSheep, Vector3 position)
    {
        HerdSheep = herdSheep;
        transform.position = new Vector3(position.x - RandomDirection(), position.y - RandomDirection(), position.z);
        transform.localScale = new Vector3(0.3f, 0.3f, transform.localScale.z);
        transform.name = "Sheep " + HerdSheep.SheepCount;
    }

    private void Start() // для теста
    {
        //Invoke("Clone", 1f);
        //Invoke("Increase", 5f);
    }

    public void Clone()
    {
        //Debug.Log("Количество овец: " + (HerdSheep.SheepCount + 1));
        HerdSheep.CloneSheep(this);
    }

    private float RandomDirection()
    {
        return Random.Range(0, 10) >= 5f ? 0.1f : -0.1f;
    }

    public void Increase()
    {
        transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z);
        HerdSheep.AddAdultSheep();
#if UNITY_EDITOR
        //Debug.Log("Adult sheep: " + HerdSheep.AdultSheepCount);
#endif
    }

    public void Die()
    {
        HerdSheep.RemoveSheep(this);
        Destroy(gameObject);
    }
}
