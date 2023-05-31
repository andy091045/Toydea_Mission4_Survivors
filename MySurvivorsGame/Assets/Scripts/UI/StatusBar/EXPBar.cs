using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : StatusBar
{
    protected override void Start()
    {
       base.Start();
       EventManager.OccurDevilGetEXPStal += UpdateEXPBar;
       UpdateEXPBar();
    }

    void UpdateEXPBar()
    {
        float length = unityData.EXP;
        float maxLength = dataManager.dataGroup.levelData[unityData.DevilLevel].Clone().NeedEXP;
        length  = length / maxLength >= 1 ? 0 : length / maxLength ;
        bar.GetComponent<Image>().fillAmount = length;
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilGetEXPStal -= UpdateEXPBar;
    }
}
