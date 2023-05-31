using HD.Pooling;
using UnityEditor;
using UnityEngine;

public class BasicPool : MonoBehaviour
{
    public string ObjectName = "";
    public IPool<GameObject> Pool { get; private set; }

    public ObjectPoolGroup objectPoolGroup_;
    
    protected virtual void Awake()
    {
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
    }

    public virtual void InstantiatePool(string name, GameObject obj, int count)
    {
        ObjectName = name;
        Pool = new ListPool<GameObject>(() => Instantiate(obj), count, g => g.activeInHierarchy, true);
    }

    public virtual void AddToGroup()
    {
        //objectPoolGroup_.AddNPCPool(this);
    }
}
