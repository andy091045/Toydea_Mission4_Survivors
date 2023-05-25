using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PlasticPipe.Server.MonitorStats;

public class VillagerStats : CharacterStats
{
    public float Speed;
    public float Attack;
    public float HP;
    public float CooldownDuration;
    bool canAttackDevil_ = false;
    float currentCooldown_;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base .Update();
        currentCooldown_ -= Time.deltaTime;
        if (currentCooldown_ <= 0)
        {            
            TryAttack();
            currentCooldown_ = CooldownDuration;
        }
    }

    
    protected override void SetInitValue()
    {
        Speed = 2f;
        HP = 30f;
        Attack = 3f;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Devil"))
        {
            canAttackDevil_ = true;
            currentCooldown_ = 0;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Devil"))
        {
            //Debug.Log("不可攻擊");
            canAttackDevil_ = false;
            currentCooldown_ = 0;
        }
    }

    protected override void TryAttack()
    {
        if (canAttackDevil_)
        {
            dataManager.dataGroup.realTimePlayerData.HP -= Attack;
            Debug.Log("玩家血量: " + dataManager.dataGroup.realTimePlayerData.HP);
        }
    }

    protected override void Move()
    {
        var direction = unityData.PlayerPos - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * Speed, Space.World);
    }

    public override void TakeDamage(float damage)
    {
        EventManager.OccurNPCGetHurt(damage.ToString(), transform.position);
        HP -= damage;
        if (HP <= 0)
        {
            Dead();
        }
    }

    protected override void Dead()
    {
        Debug.Log("村民死了");
        Destroy(gameObject);
    }
}
