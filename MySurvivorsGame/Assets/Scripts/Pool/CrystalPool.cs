using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPool : BasicPool
{
    Transform pools_;

    protected override void Awake()
    {
        base.Awake();
        pools_ = GameObject.Find("Pools").transform;
    }

    public override void AddToGroup()
    {
        objectPoolGroup_.AddCrystalPool(this);
        gameObject.transform.parent = pools_;   
    }
}
