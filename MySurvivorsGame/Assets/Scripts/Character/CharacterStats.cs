using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public DataManager dataManager_;

    protected virtual void Start()
    {
        dataManager_ = GameContainer.Get<DataManager>();
        SetInitValue();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void SetInitValue()
    {
        
    }

    protected virtual void Move()
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
