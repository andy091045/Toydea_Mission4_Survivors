﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : StatusBar
{
    protected override void Start()
    {
        base.Start();
        unityData.EXP.OnValueChanged += UpdateEXPBar;
        UpdateEXPBar(unityData.EXP.Value);
    }

    void UpdateEXPBar(float exp)
    {
        float length = exp;
        float maxLength = dataManager.dataGroup.levelData[unityData.DevilLevel.Value].Clone().NeedEXP;
        length = length / maxLength >= 1 ? 0 : length / maxLength;
        if (bar != null)
        {
            bar.GetComponent<Image>().fillAmount = length;
        }        
    }

    private void OnDestroy()
    {
        unityData.EXP.OnValueChanged += UpdateEXPBar;
    }
}

