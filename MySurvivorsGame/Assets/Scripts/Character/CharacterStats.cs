using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public DataManager dataManager;
    public UnityData unityData;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        dataManager = GameContainer.Get<DataManager>();
        unityData = GameContainer.Get<UnityData>();
        SetInitValue();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void SetInitValue()
    {
        SetInitSkill();
    }

    protected virtual void SetInitSkill()
    {
        
    }

    protected virtual void Move()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {

    }

    protected virtual void TryAttack()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        //HP -= damage;
        //if(HP <= 0)
        //{
        //    Dead();
        //}
    }

    protected virtual void Dead()
    {

    }
}
