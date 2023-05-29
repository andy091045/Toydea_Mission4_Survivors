using Codice.CM.Common;
using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PlasticPipe.Server.MonitorStats;

public class VillagerStats : NPCStats
{
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
            currentCooldown_ = npcPoolData.CooldownDuration;
        }
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
            dataManager.dataGroup.realTimePlayerData.HP -= npcPoolData.Attack;
            Debug.Log("玩家血量: " + dataManager.dataGroup.realTimePlayerData.HP);
        }
    }

    protected override void Move()
    {
        var direction = unityData.PlayerPos - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * npcPoolData.Speed, Space.World);
    }

    public override void TakeDamage(float damage)
    {
        if(npcPoolData.HP <= 0)
        {
            return;
        }
        EventManager.OccurNPCGetHurt(damage.ToString(), transform.position);

        npcPoolData.HP -= damage;
        if (npcPoolData.HP <= 0)
        {
            Dead();
        }
    }

    protected override void Dead()
    {
        //Debug.Log("村民死了");
        unityData.VillagersNumber--;
        gameObject.SetActive(false);
    }
}
