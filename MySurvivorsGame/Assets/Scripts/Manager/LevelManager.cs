using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    DataManager dataManager_;
    UnityData unityData_;

    private void Start()
    {
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_ = GameContainer.Get<UnityData>();
        EventManager.EXP.OnValueChanged += UpdateDevilLevel;
    }


    private void UpdateDevilLevel(float exp)
    {
        float needEXP = dataManager_.dataGroup.levelData[EventManager.DevilLevel.Value].NeedEXP;
        if(needEXP > exp)
        {
            return;
        }
        EventManager.EXP.Value = exp - needEXP;
        EventManager.DevilLevel.Value++;
    }

    private void OnDestroy()
    {
        EventManager.EXP.OnValueChanged -= UpdateDevilLevel;
    }
}
