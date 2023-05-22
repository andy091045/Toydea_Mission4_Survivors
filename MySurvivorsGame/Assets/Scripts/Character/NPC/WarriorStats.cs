using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStats : CharacterStats
{
    Transform target_;
    protected override void Start()
    {
        base.Start();
        target_ = FindObjectOfType<PlayerTest>().transform;
    }

    protected override void SetInitValue()
    {
        Speed = 2f;
        HP = 10f;
        Attack = 3f;
    }

    protected override void Move()
    {
        var direction = target_.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * Speed, Space.World);
    }

    protected override void Dead()
    {

    }
}
