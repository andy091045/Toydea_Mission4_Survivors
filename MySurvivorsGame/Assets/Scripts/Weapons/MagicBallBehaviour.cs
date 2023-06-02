using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : ProjectileWeaponBehaviour
{
    void Update()
    {
        if (KeyInputManager.Instance.IsObjectCanMove)
        {
            transform.position += direction * weaponLevelData.Speed * Time.deltaTime;
        }        
    }
}
