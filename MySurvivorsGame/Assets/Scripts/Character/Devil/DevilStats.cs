using System.Collections;
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
        devilData_ = GameContainer.Get<DataManager>().dataGroup.realTimePlayerData;
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

    public override void TakeDamage(float damage)
    {
        devilData_.HP -= damage;
        Debug.Log("玩家受傷，剩餘HP為: " + devilData_.HP);
        if (devilData_.HP <= 0)
        {
            Debug.Log("玩家死亡");
            Dead();
        }
    }

    protected override void Dead()
    {

    }
}
