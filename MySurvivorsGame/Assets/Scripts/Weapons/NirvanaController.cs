using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NirvanaController : WeaponController
{
    //Nirvana
    float nirvanaTime_ = 10.0f;
    GameObject nirvana_;

    protected override void Start()
    {
        //Nirvana
        InitNirvana();
        KeyInputManager.Instance.IsInNirvanaTime.OnValueChanged += UseNirvana;
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
        nirvana_.transform.position = unityData.PlayerPos;
        yield return new WaitForSeconds(nirvanaTime_);
        nirvana_.SetActive(false);
        KeyInputManager.Instance.IsInNirvanaTime.Value = false;
        unityData.NowDevilData.Attack /= 5;
        unityData.NowDevilData.AttackCooldown *= 10;
    }

    private void UseNirvana(bool isUseNirvana)
    {
        if (isUseNirvana)
        {
            StartCoroutine(PlayNirvana());
        }        
    }

    private void OnDestroy()
    {
        KeyInputManager.Instance.IsInNirvanaTime.OnValueChanged -= UseNirvana;
    }
}
