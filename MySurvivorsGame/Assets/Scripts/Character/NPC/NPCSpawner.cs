using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NPCSpawner : MonoBehaviour
{
    DataManager data_;
    ObjectPoolGroup objectPoolGroup_;

    List<PoolData> poolData_ = new List<PoolData>();

    private void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        
        poolData_ = data_.dataGroup.poolsData;
        InitializePools();       
    }

    async void InitializePools()
    {
        for (int i = 0; i < poolData_.Count; i++)
        {            
            GameObject pool = new GameObject(poolData_[i].CharacterName + "Pool");
            pool.AddComponent<BasicPool>();
            pool.GetComponent<BasicPool>().Prefab = await Addressables.LoadAssetAsync<GameObject>(poolData_[i].ObjectPrefabPath).Task;
            pool.GetComponent<BasicPool>().Count = poolData_[i].CharacterCount;
            pool.GetComponent<BasicPool>().InstantiateAndAddToGroup();
            pool.transform.parent = transform;
        }
        AddPrefabToGame();
    }

    void AddPrefabToGame()
    {
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        if (objectPoolGroup_.objectPools_.Count != 0)
        {
            Debug.Log(objectPoolGroup_.objectPools_[0].Count);
            objectPoolGroup_.objectPools_[0].Pool.GetInstance();
        }
    }
}
