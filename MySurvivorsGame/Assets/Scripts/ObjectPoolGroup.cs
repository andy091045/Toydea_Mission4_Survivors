using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolGroup
{
    public List<ObjectPool> objectPools_ = new List<ObjectPool>();    
    public void AddPool(ObjectPool objectPool)
    {
        objectPools_.Add(objectPool);
    }
}
