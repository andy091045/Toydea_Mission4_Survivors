using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCrystal : CryStal
{
    protected override void Start()
    {
        base.Start();
        expValue = dataManager.dataGroup.crystalsData[2].Clone().EXPValue;
    }
}
