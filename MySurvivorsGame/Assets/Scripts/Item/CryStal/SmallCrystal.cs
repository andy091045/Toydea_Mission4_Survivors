using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrystal : CryStal
{
    protected override void Start()
    {
        base.Start();
        expValue = dataManager.dataGroup.crystalsData[0].Clone().EXPValue * unityData.NowDevilData.ExpEffect;
    }
}
