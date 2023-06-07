using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelManager : MonoBehaviour
{
    DataManager dataManager_;
    UnityData unityData_;

    AudioSource audioSource_;

    private void Start()
    {
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_ = GameContainer.Get<UnityData>();
        unityData_.EXP.OnValueChanged += UpdateDevilLevel;
        audioSource_ = gameObject.AddComponent<AudioSource>();
        GetSE();
    }

    async void GetSE()
    {
        AudioClip clip = await Addressables.LoadAssetAsync<AudioClip>("Assets/Music/LevelUp_Music.mp3").Task;
        audioSource_.clip = clip;
        audioSource_.volume *= 0.3f;
    }

    private void UpdateDevilLevel(float exp)
    {
        float needEXP = dataManager_.dataGroup.levelData[unityData_.DevilLevel.Value].NeedEXP;
        if(needEXP > exp)
        {
            return;
        }
        unityData_.EXP.Value = exp - needEXP;
        audioSource_.Play();
        unityData_.DevilLevel.Value++;
    }

    private void OnDestroy()
    {
        unityData_.EXP.OnValueChanged -= UpdateDevilLevel;
    }
}
