using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : StatusBar
{
    protected override void Start()
    {
        base.Start();
        EventManager.EXP.OnValueChanged += UpdateEXPBar;
        UpdateEXPBar(EventManager.EXP.Value);
    }

    void UpdateEXPBar(float exp)
    {
        float length = exp;
        float maxLength = dataManager.dataGroup.levelData[EventManager.DevilLevel.Value].Clone().NeedEXP;
        length = length / maxLength >= 1 ? 0 : length / maxLength;
        bar.GetComponent<Image>().fillAmount = length;
    }

    private void OnDestroy()
    {
        EventManager.EXP.OnValueChanged += UpdateEXPBar;
    }
}

