﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

public class DevilStats : CharacterStats
{
    public string DevilName = "";

    [SerializeField] private DevilData devilData_;
    [SerializeField] private Vector3 playerDir_ = Vector3.zero;
    [SerializeField] private Vector3 previousPlayerDir_ = Vector3.zero;
    [SerializeField] private Vector3 movementVector_;
    Rigidbody2D rgbd2d_;

    protected override void Awake()
    {
        base.Awake();
        rgbd2d_ = GetComponent<Rigidbody2D>();
        movementVector_ = new Vector3();
    }

    protected override void Start()
    {
        base .Start();
        KeyInputManager.Instance.onHorizontalMoveEvent.AddListener(GetHorizontalValue);
        KeyInputManager.Instance.onVerticalMoveEvent.AddListener(GetVerticalValue);
    }

    void GetHorizontalValue(float h)
    {
        movementVector_.x = h;
    }

    void GetVerticalValue(float v)
    {
        movementVector_.y = v;
    }

    protected override void SetInitValue()
    {
        var DevilsData = dataManager.dataGroup.devilsData;
        for (int i = 0; i < DevilsData.Count; i++)
        {
            if (DevilsData[i].DevilName == DevilName)
            {
                devilData_ = DevilsData[i];
                //Debug.Log("設定" + devilData.DevilName + "數值");
                break;
            }
        }

        if (devilData_.HP == 0)
        {
            Debug.LogWarning("找不到" + devilData_.DevilName + "的資料");
        }
    }

    protected override void Move()
    {
        playerDir_ = new Vector2(movementVector_.x, movementVector_.y).normalized;

        if (playerDir_ != Vector3.zero)
        {
            previousPlayerDir_ = playerDir_;
        }
        unityData.PlayerDir = playerDir_;
        unityData.PreviousPlayerDir = previousPlayerDir_;
        unityData.PlayerPos = transform.position;

        rgbd2d_.velocity = new Vector2(playerDir_.x * devilData_.Speed, playerDir_.y * devilData_.Speed);
    }

    protected override void Dead()
    {

    }
}
