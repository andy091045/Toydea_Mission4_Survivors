using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
            if (unityData.NowDevilData.DamageCut < npcPoolData.Attack)
            {
                unityData.NowDevilData.HP = unityData.NowDevilData.HP + unityData.NowDevilData.DamageCut - npcPoolData.Attack;
                EventManager.OccurDevilHPChange.Invoke(true);
            }
            //unityData.NowDevilData.HP -= npcPoolData.Attack;
            //EventManager.OccurDevilGetHurt.Invoke();
            Debug.Log("敵人攻擊力: " + npcPoolData.Attack);
            Debug.Log("玩家血量: " + unityData.NowDevilData.HP);
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
        int result = (int)damage;
        EventManager.OccurNPCGetHurt(result.ToString(), transform.position);

        npcPoolData.HP -= damage;
        if (npcPoolData.HP <= 0)
        {
            Dead();
        }
    }

    protected override void Dead()
    {
        base.Dead();
        //Debug.Log("村民死了");
        unityData.VillagersNumber--;
        canAttackDevil_ = false;
        gameObject.SetActive(false);
    }
}
