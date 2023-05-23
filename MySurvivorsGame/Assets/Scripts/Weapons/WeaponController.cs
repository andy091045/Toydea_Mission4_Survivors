using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script for all weapon controllers
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject Prefab;
    public float Damage;
    public float Speed;
    public float CooldownDuration;
    public int Pierce;
    float currentCooldown_;

    public UnityData unityData_;
    
    protected virtual void Awake()
    {
        unityData_ = GameContainer.Get<UnityData>();
    }

    protected virtual void Start()
    {
        currentCooldown_ = CooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown_ -= Time.deltaTime;
        if(currentCooldown_ < 0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown_ = CooldownDuration;
    }
}
