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
        EventManager.OccurDevilGetEXPStal += ChangeText;
    }

    void ChangeText()
    {
        Debug.Log(unityData_.DevilLevel);
        Number.text = unityData_.DevilLevel.ToString();
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilGetEXPStal -= ChangeText;
    }
}
