using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NPCSpawner : MonoBehaviour
{
    DataManager data_;
    ObjectPoolGroup objectPoolGroup_;

    List<NPCPoolData> poolData_ = new List<NPCPoolData>();

    private void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        poolData_ = data_.dataGroup.npcPoolsData;
        InitializePools();       
    }

    async void InitializePools()
    {
        for (int i = 0; i < poolData_.Count; i++)
        {            
            GameObject pool = new GameObject(poolData_[i].CharacterName + "Pool");
            pool.AddComponent<BasicPool>();
            pool.GetComponent<BasicPool>().Prefab = await Addressables.LoadAssetAsync<GameObject>(poolData_[i].ObjectPrefabPath).Task;
            //System.Type type = System.Type.GetType(poolData_[i].ClassName);
            //pool.GetComponent<BasicPool>().Prefab.AddComponent();
            pool.GetComponent<BasicPool>().Count = poolData_[i].CharacterCount;
            pool.GetComponent<BasicPool>().InstantiateAndAddToGroup();
            pool.transform.parent = transform;
        }
        AddPrefabToGame();
    }

    void AddPrefabToGame()
    {
        if (objectPoolGroup_.objectPools_.Count != 0)
        {
            Debug.Log(objectPoolGroup_.objectPools_[0].Count);
            var apple = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
            apple.GetComponent<VillagerStats>().SetNPCValue(poolData_[0]);
        }
    }
}
