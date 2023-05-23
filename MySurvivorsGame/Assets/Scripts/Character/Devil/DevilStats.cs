using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

public class DevilStats : CharacterStats
{
    public string DevilName = "";
    public DevilData devilData;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetInitValue()
    {
        var DevilsData = dataManager_.dataGroup.devilsData;
        for (int i = 0; i < DevilsData.Count; i++)
        {
            if (DevilsData[i].DevilName == DevilName)
            {
                devilData = DevilsData[i];
                Debug.Log("設定" + devilData.DevilName + "數值");
                break;
            }
        }

        if (devilData.HP == 0)
        {
            Debug.LogWarning("找不到" + devilData.DevilName + "的資料");
        }
    }

    protected override void Move()
    {
       
    }

    protected override void Dead()
    {

    }
}
