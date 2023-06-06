using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : ProjectileWeaponBehaviour
{
    void Update()
    {
        transform.RotateAround(unityData.PlayerPos, Vector3.forward, weaponLevelData.Speed * Time.deltaTime);
    }
}
