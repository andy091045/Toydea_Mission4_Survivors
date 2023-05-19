using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolGroup
{
    public List<BulletPool> objectPools_ = new List<BulletPool>();    
    public void AddPool(BulletPool objectPool)
    {
        objectPools_.Add(objectPool);
    }
}
