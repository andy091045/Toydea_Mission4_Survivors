using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NirvanaController : WeaponController
{
    //Nirvana
    float nirvanaTime_ = 10.0f;
    GameObject nirvana_;
    bool canMoveNirvana_ = false;

    protected override void Start()
    {
        //Nirvana
        InitNirvana();
        KeyInputManager.Instance.onNirvanaUseEvent.AddListener(UseNirvana);
    }

    async void InitNirvana()
    {
        nirvana_ = Instantiate(await Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Skill/Fire.prefab").Task);
        nirvana_.SetActive(false);
    }

    protected override void Update()
    {
        if (canMoveNirvana_)
        {
            nirvana_.transform.position = unityData.PlayerPos;
        }
    }

    private IEnumerator PlayNirvana()
    {
        nirvana_.SetActive(true);
        dataManager.dataGroup.realTimePlayerData.Attack *= 2;
        canMoveNirvana_ = true;
        nirvana_.transform.position = unityData.PlayerPos;
        yield return new WaitForSeconds(nirvanaTime_);
        nirvana_.SetActive(false);
        dataManager.dataGroup.realTimePlayerData.Attack /= 2;
        canMoveNirvana_ = false;
    }

    private void UseNirvana()
    {
        StartCoroutine(PlayNirvana());
    }
}
