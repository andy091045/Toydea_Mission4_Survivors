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
        string prefabPath = ""; 
        if(data_.dataGroup.realTimePlayerData.ChooseDevil == "Reaper")
        {
            prefabPath = "Assets/ArtResources/Devils/Reaper/Prefab/Reaper.prefab";
        }else if(data_.dataGroup.realTimePlayerData.ChooseDevil == "BoneMan")
        {
            prefabPath = "Assets/ArtResources/Devils/BoneMan/Prefab/BoneMan.prefab";
        }
        GameObject prefabObj = await Addressables.LoadAssetAsync<GameObject>(prefabPath).Task;
        GameObject devilObject = Instantiate(prefabObj);
        devilObject.AddComponent<PlayerTest>();

        EventManager.OccurInstantiateDevil.Invoke();
    }
}
