using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemManager : MonoBehaviour
{
    DataManager dataManager_;

    private void Start()
    {
        dataManager_ = GameContainer.Get<DataManager>();
        InitializeCrystalPools();
    }

    async void InitializeCrystalPools()
    {
        for (int i = 0; i < dataManager_.dataGroup.crystalsData.Count; i++)
        {
            GameObject crystalPool = new GameObject(dataManager_.dataGroup.crystalsData[i].CrystalName + "Pool");
            crystalPool.AddComponent<BasicPool>();
            crystalPool.GetComponent<BasicPool>().Prefab = await Addressables.LoadAssetAsync<GameObject>(dataManager_.dataGroup.crystalsData[i].PrefabPath).Task;

            crystalPool.GetComponent<BasicPool>().Count = dataManager_.dataGroup.crystalsData[i].Count;
            crystalPool.GetComponent<BasicPool>().InstantiateAndAddToGroup();
        }
    }
}
