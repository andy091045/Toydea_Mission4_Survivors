using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NirvanaController : WeaponController
{
    //Nirvana
    float nirvanaTime_ = 10.0f;
    GameObject nirvana_;

    /// <summary>
    /// ひさつわざの使用に必要な消費量
    /// </summary>
    int useNirvanaComsume = 100;

    protected override void Start()
    {
        //Nirvana
        InitNirvana();
        KeyInputManager.Instance.onNirvanaUseEvent.AddListener(IsInNirvanaTimeInvoke);
        unityData.IsInNirvanaTime.OnValueChanged += UseNirvana;
    }

    async void InitNirvana()
    {
        nirvana_ = Instantiate(await Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Skill/Fire2_6.prefab").Task);
        nirvana_.transform.parent = transform.parent;
        nirvana_.SetActive(false);
    }


    private IEnumerator PlayNirvana()
    {
        nirvana_.SetActive(true);
        unityData.NowDevilData.Attack *= 5;
        unityData.NowDevilData.AttackCooldown /= 10;
        unityData.IsInNirvana = true;
        nirvana_.transform.position = unityData.PlayerPos;
        //
        unityData.TotalDeadCount.Value -= useNirvanaComsume;
        yield return new WaitForSeconds(nirvanaTime_);
        nirvana_.SetActive(false);
        unityData.IsInNirvanaTime.Value = false;
        unityData.NowDevilData.Attack /= 5;
        unityData.NowDevilData.AttackCooldown *= 10;
        unityData.IsInNirvana = false;
    }

    private void UseNirvana(bool isUseNirvana)
    {
        if (isUseNirvana)
        {
            StartCoroutine(PlayNirvana());
        }        
    }

    void IsInNirvanaTimeInvoke()
    {
        if(unityData.TotalDeadCount.Value > useNirvanaComsume)
        {
            unityData.IsInNirvanaTime.Value = true;
        }        
    }

    private void OnDestroy()
    {
        unityData.IsInNirvanaTime.OnValueChanged -= UseNirvana;
    }
}
