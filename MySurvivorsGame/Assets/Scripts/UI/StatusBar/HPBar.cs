using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : StatusBar
{
    protected override void Start()
    {
        base.Start();
        EventManager.OccurDevilHPChange += AdjustHP;
    }

    void AdjustHP(bool i)
    {
        SetState(unityData.NowDevilData.Clone().HP, dataManager.dataGroup.realTimePlayerData.Clone().HP);
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilHPChange -= AdjustHP;
    }
}
