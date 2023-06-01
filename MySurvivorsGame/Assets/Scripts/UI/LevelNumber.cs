using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    public Text Number;

    UnityData unityData_;
    
    void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        unityData_.DevilLevel.OnValueChanged += ChangeText;
    }

    void ChangeText(int level)
    {
        Number.text = level.ToString();
    }

    private void OnDestroy()
    {
        unityData_.DevilLevel.OnValueChanged -= ChangeText;
    }
}
