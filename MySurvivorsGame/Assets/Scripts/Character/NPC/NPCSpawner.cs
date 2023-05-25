using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    DataManager data_;

    List<PoolData> poolData_ = new List<PoolData>();

    private void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        poolData_ = data_.dataGroup.poolsData;
        InitializePool();
    }

    void InitializePool()
    {

    }
}
