using DataProcess;
using HD.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.MaterialProperty;

public class BulletPool : MonoBehaviour
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
