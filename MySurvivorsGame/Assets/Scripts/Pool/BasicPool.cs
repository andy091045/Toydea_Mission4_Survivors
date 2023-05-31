using HD.Pooling;
using UnityEngine;

public class BasicPool : MonoBehaviour
{
    public GameObject Prefab;
    public int Count;
    public IPool<GameObject> Pool { get; private set; }

    ObjectPoolGroup objectPoolGroup_;
    
    private void Awake()
    {
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
    }

    public void InstantiateAndAddToGroup()
    {
        Pool = new ListPool<GameObject>(() => Instantiate(Prefab), Count, g => g.activeInHierarchy, true);
        objectPoolGroup_.AddNPCPool(this);
    }
}
