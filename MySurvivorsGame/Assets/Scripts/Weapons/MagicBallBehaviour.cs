using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : ProjectileWeaponBehaviour
{
    MagicBallController mc;

    protected override void Start()
    {
        base.Start();
        mc = FindObjectOfType<MagicBallController>();
    }

    
    void Update()
    {
        transform.position += direction * mc.Speed * Time.deltaTime;
    }
}
