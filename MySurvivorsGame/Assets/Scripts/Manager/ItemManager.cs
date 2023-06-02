using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemManager : MonoBehaviour
{
    DataManager dataManager_;
    UnityData unityData_;
    ObjectPoolGroup objectPoolGroup_;

    private void Start()
    {
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_ = GameContainer.Get<UnityData>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        //Initialize_UnityData_HoldItems();
        InitializeCrystalPools();
    }

    //void Initialize_UnityData_HoldItems()
    //{
    //    unityData_.HoldItems = dataManager_.dataGroup.itemsData;
    //}

    async void InitializeCrystalPools()
    {
        for (int i = 0; i < dataManager_.dataGroup.crystalsData.Count; i++)
        {
            GameObject obj = new GameObject(dataManager_.dataGroup.crystalsData[i].CrystalName + "Pool");
            CrystalPool crystalPool = obj.AddComponent<CrystalPool>();

            string crystalName = dataManager_.dataGroup.crystalsData[i].Clone().CrystalName;
            GameObject prefab = await Addressables.LoadAssetAsync<GameObject>(dataManager_.dataGroup.crystalsData[i].PrefabPath).Task;
            int count = dataManager_.dataGroup.crystalsData[i].Clone().Count;
            crystalPool.InstantiatePool(crystalName, prefab, count);
            crystalPool.AddToGroup();
        }
    }
}
