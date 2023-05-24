using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolGroup
{
    public List<BasicPool> objectPools_ = new List<BasicPool>();    
    public void AddPool(BasicPool objectPool)
    {
        objectPools_.Add(objectPool);
    }
}
