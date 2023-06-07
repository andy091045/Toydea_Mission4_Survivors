using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MagicBallController : WeaponController
{
    AudioSource audioSource_;

    protected override void Start()
    {
        WeaponName = "MagicBall";
        base.Start();
        SetSE();
    }

    async void SetSE()
    {
        audioSource_ = gameObject.AddComponent<AudioSource>();
        AudioClip clip = await Addressables.LoadAssetAsync<AudioClip>("Assets/Music/Shoot_MagicBall_Sound.mp3").Task;
        audioSource_.clip = clip;
    }

    protected override void Attack()
    {        
        base.Attack();
        if (unityData.PreviousPlayerDir != Vector3.zero)
        {
            audioSource_.Play();
            for (int i = 0; i < CurrentWeaponLevelData.Number; i++)
            {
                GameObject spawnedMagicBall = Instantiate(Prefab);
                spawnedMagicBall.transform.position = transform.position;
                spawnedMagicBall.GetComponent<MagicBallBehaviour>().DirectionChecker(CurrentWeaponLevelData.Number, i);
                spawnedMagicBall.GetComponent<MagicBallBehaviour>().weaponLevelData = CurrentWeaponLevelData;
                spawnedMagicBall.GetComponent<MagicBallBehaviour>().weaponData = weaponData;
            }            
        }        
    }
}
