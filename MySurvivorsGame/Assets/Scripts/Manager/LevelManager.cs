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
        EventManager.OccurDevilGetEXPStal += UpdateDevilLevel;
    }


    private void UpdateDevilLevel()
    {
        float needEXP = dataManager_.dataGroup.levelData[unityData_.DevilLevel].NeedEXP;
        if(needEXP > unityData_.EXP)
        {
            return;
        }
        unityData_.EXP = unityData_.EXP - needEXP;
        unityData_.DevilLevel++;
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilGetEXPStal -= UpdateDevilLevel;
    }
}
