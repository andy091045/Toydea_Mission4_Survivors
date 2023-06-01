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
        unityData_.EXP.OnValueChanged += UpdateDevilLevel;
    }


    private void UpdateDevilLevel(float exp)
    {
        float needEXP = dataManager_.dataGroup.levelData[unityData_.DevilLevel.Value].NeedEXP;
        if(needEXP > exp)
        {
            return;
        }
        unityData_.EXP.Value = exp - needEXP;
        unityData_.DevilLevel.Value++;
    }

    private void OnDestroy()
    {
        unityData_.EXP.OnValueChanged -= UpdateDevilLevel;
    }
}
