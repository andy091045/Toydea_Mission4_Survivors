﻿using DataDefinition;
using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterManager : MonoBehaviour
{
    DataManager data_;
    UnityData unitydata_;

    void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        unitydata_ = GameContainer.Get<UnityData>();
        InstantiateGameObjectComponent();
        InstantiateDevil();
    }

    void InstantiateGameObjectComponent()
    {
        GameObject npcSpawner = new GameObject("NPCSpawner");
        npcSpawner.AddComponent<NPCSpawner>();
    }

    private async void InstantiateDevil()
    {
        var DevilsData = data_.dataGroup.devilsData;
        for (int i = 0; i < DevilsData.Count; i++)
        {
            if (DevilsData[i].DevilName == data_.dataGroup.realTimePlayerData.DevilName)
            {
                data_.dataGroup.realTimePlayerData = DevilsData[i].Clone();
                unitydata_.NowDevilData = DevilsData[i].Clone();
                break;
            }
        }

        if (data_.dataGroup.realTimePlayerData.HP == 0)
        {
            Debug.LogWarning("找不到" + data_.dataGroup.realTimePlayerData.DevilName + "的資料");
        }

        GameObject prefabObj = await Addressables.LoadAssetAsync<GameObject>(data_.dataGroup.realTimePlayerData.PrefabPath).Task;
        GameObject devilObject = Instantiate(prefabObj);
        
        devilObject.AddComponent<DevilStats>();        
        EventManager.OccurInstantiateDevil.Invoke();
    }
}
