using HD.Pooling;
using UnityEngine;

public class BasicPool : MonoBehaviour
{
    public GameObject Prototype;
    public int capacity;
    public IPool<GameObject> Pool { get; private set; }

    ObjectPoolGroup objectPoolGroup;
    
    private void Awake()
    {
        objectPoolGroup =  GameContainer.Get<ObjectPoolGroup>();
        Pool = new ListPool<GameObject>(() => Instantiate(Prototype), capacity, g => g.activeInHierarchy, true);
    }

    void Start()
    {
        objectPoolGroup.AddPool(this);
    }
}
