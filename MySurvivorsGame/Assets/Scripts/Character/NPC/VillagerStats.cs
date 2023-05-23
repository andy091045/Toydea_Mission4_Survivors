using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticPipe.Server.MonitorStats;

public class VillagerStats : CharacterStats
{
    public float Speed;
    public float Attack;
    public float HP;

    UnityData unityData_;

    protected override void Start()
    {
        base.Start();
        unityData_ = GameContainer.Get<UnityData>();
    }

    protected override void SetInitValue()
    {
        Speed = 2f;
        HP = 10f;
        Attack = 3f;
    }

    protected override void Move()
    {
        var direction = unityData_.PlayerPos - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * Speed, Space.World);
    }

    protected override void Dead()
    {

    }
}
