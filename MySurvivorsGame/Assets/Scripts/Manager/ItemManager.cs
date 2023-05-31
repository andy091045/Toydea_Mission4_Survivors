using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemManager : MonoBehaviour
{
    DataManager dataManager_;
    ObjectPoolGroup objectPoolGroup_;

    private void Start()
    {
        dataManager_ = GameContainer.Get<DataManager>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        InitializeCrystalPools();
    }

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
