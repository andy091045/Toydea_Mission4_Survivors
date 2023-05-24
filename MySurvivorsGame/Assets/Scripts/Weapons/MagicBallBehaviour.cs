using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : ProjectileWeaponBehaviour
{
    void Update()
    {
        transform.position += direction *  weaponLevelData.Speed * Time.deltaTime;
    }
}
