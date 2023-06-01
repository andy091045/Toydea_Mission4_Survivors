using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : StatusBar
{
    protected override void Start()
    {
        base.Start();
        EventManager.OccurDevilGetHurt += AdjustHP;
    }

    void AdjustHP()
    {
        SetState(unityData.NowDevilData.HP, dataManager.dataGroup.realTimePlayerData.HP);
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilGetHurt -= AdjustHP;
    }
}
