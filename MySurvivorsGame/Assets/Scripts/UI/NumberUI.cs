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
        unityData_.DevilLevel.OnValueChanged += level => ChangeText(LevelNumber, level);
        unityData_.TotalDeadCount.OnValueChanged += deadCount => ChangeText(DeadNumber, deadCount);
    }
    
    void ChangeText(Text text, int level)
    {
        text.text = level.ToString();
    }

    private void OnDestroy()
    {
        unityData_.DevilLevel.OnValueChanged -= level => ChangeText(LevelNumber, level);
        unityData_.TotalDeadCount.OnValueChanged -= deadCount => ChangeText(LevelNumber, deadCount);
    }
}
