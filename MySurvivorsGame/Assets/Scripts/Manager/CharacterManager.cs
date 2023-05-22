using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterManager : MonoBehaviour
{
    DataManager data_;

    void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        InstantiateDevil();
    }

    private async void InstantiateDevil()
    {
        string prefabPath = data_.dataGroup.realTimePlayerData.PrefabPath;
        Debug.Log(prefabPath);
        GameObject prefabObj = await Addressables.LoadAssetAsync<GameObject>(prefabPath).Task;
        GameObject devilObject = Instantiate(prefabObj);
        devilObject.AddComponent<PlayerTest>();

        EventManager.OccurInstantiateDevil.Invoke();
    }
}
