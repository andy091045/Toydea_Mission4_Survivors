using DataDefinition;
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
                data_.dataGroup.realTimePlayerData = DevilsData[i];
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
        devilObject.GetComponent<DevilStats>().DevilName = data_.dataGroup.realTimePlayerData.DevilName;
        EventManager.OccurInstantiateDevil.Invoke();
    }
}
