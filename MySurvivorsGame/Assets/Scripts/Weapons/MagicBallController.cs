using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        if (pt.PreviousPlayerDir != Vector3.zero)
        {
            GameObject spawnedMagicBall = Instantiate(Prefab);
            spawnedMagicBall.transform.position = transform.position;
            spawnedMagicBall.GetComponent<MagicBallBehaviour>().DirectionChecker(pt.PlayerDir, pt.PreviousPlayerDir);
        }        
    }
}
