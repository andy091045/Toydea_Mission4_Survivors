using log4net.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberUI : MonoBehaviour
{
    public Text LevelNumber;
    public Text DeadNumber;

    UnityData unityData_;
    
    void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        //unityData_.DevilLevel.OnValueChanged += level => ChangeText(LevelNumber, level);
        //unityData_.TotalDeadCount.OnValueChanged += deadCount => ChangeText(DeadNumber, deadCount);
        unityData_.DevilLevel.OnValueChanged += ChangeText;
        unityData_.TotalDeadCount.OnValueChanged += ChangeDeadText;
    }
    
    void ChangeText(int i)
    {
        LevelNumber.text = i.ToString();
    }

    void ChangeDeadText(int i)
    {
        DeadNumber.text = i.ToString();
    }

    private void OnDestroy()
    {
        //unityData_.DevilLevel.OnValueChanged -= level => ChangeText(LevelNumber, level);
        //unityData_.TotalDeadCount.OnValueChanged -= deadCount => ChangeText(LevelNumber, deadCount);
        unityData_.DevilLevel.OnValueChanged -= ChangeText;
        unityData_.TotalDeadCount.OnValueChanged -= ChangeDeadText;
    }
}
