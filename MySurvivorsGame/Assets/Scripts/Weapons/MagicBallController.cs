using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallController : WeaponController
{
    
    protected override void Start()
    {
        WeaponName = "MagicBall";
        base.Start();        
    }

    protected override void Attack()
    {
        base.Attack();
        if (unityData.PreviousPlayerDir != Vector3.zero)
        {
            for(int i = 0; i < CurrentWeaponLevelData.Number; i++)
            {
                GameObject spawnedMagicBall = Instantiate(Prefab);
                spawnedMagicBall.transform.position = transform.position;
                spawnedMagicBall.GetComponent<MagicBallBehaviour>().DirectionChecker(CurrentWeaponLevelData.Number, i);
            }            
        }        
    }
}
