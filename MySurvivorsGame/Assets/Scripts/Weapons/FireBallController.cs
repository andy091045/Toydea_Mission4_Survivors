using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : WeaponController
{
    protected override void Start()
    {
        WeaponName = "FireBall";
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        if (unityData.PreviousPlayerDir != Vector3.zero)
        {
            for (int i = 0; i < CurrentWeaponLevelData.Number; i++)
            {
                Debug.LogWarning(Prefab.name);
                GameObject spawnedMagicBall = Instantiate(Prefab);
                spawnedMagicBall.transform.position = transform.position;
                spawnedMagicBall.GetComponent<MagicBallBehaviour>().DirectionChecker(CurrentWeaponLevelData.Number, i);
                spawnedMagicBall.GetComponent<MagicBallBehaviour>().weaponLevelData = CurrentWeaponLevelData;
            }
        }
    }
}