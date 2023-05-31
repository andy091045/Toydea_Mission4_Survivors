using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolGroup
{
    public List<BasicPool> NPCPools= new List<BasicPool>();    
    public void AddNPCPool(BasicPool objectPool)
    {
        NPCPools.Add(objectPool);
    }
}
