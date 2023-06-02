﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;
using UnityEngine.AddressableAssets;
using NaughtyAttributes;

public class DevilStats : CharacterStats, IHaveHPBar    
{
    [Label("イニシャル魔王資料")]
    [SerializeField] private DevilData devilData_;

    //[Label("魔王ランク")]
    //[SerializeField] private int devilLevel_ = 0;

    Vector3 playerDir_ = Vector3.zero;
    Vector3 previousPlayerDir_ = Vector3.zero;
    Vector3 movementVector_;
    Rigidbody2D rgbd2d_;

    /// <summary>
    /// 負傷
    /// </summary>
    SpriteRenderer spriteRenderer_;
    Material flashMaterial_;
    Material originalMaterial_;
    Coroutine flashRoutine_;
    float flashDuration_ = 0.06f;
    

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
        EventManager.OccurDevilGetHurt += Flash;
        CreateHPBar();

        spriteRenderer_ = GetComponentInChildren<SpriteRenderer>();
        originalMaterial_ = spriteRenderer_.material;
        AddFlashMaterial();
    }

    protected override void Update()
    {
        base.Update();             
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
        base.SetInitValue();
    }

    protected override void SetInitSkill()
    {
        System.Type type;
        for (int i = 0; i < dataManager.dataGroup.weaponsData.Count; i++)
        {
            if(devilData_.InitWeapon == dataManager.dataGroup.weaponsData[i].WeaponName)
            {
                dataManager.dataGroup.weaponsData[i].NowWeaponLevel++;
                type = System.Type.GetType(dataManager.dataGroup.weaponsData[i].ScriptName);
                AddWeaponController(devilData_.InitWeapon, type);
            }
        }        

        type = System.Type.GetType("NirvanaController");
        AddWeaponController("NirvanaController", type);
    }

    void AddWeaponController(string name, System.Type weaponType)
    {
        GameObject weaponController = new GameObject(name);
        weaponController.transform.parent = transform;
        weaponController.transform.position = transform.position;
        weaponController.AddComponent(weaponType);
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

    public async void CreateHPBar()
    {
        GameObject HPPrefab = await Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Devils/HP.prefab").Task;
        GameObject obj = Instantiate(HPPrefab);
        obj.transform.parent = transform;
        obj.transform.position = new Vector3(transform.position.x -0.9f, transform.position.y + 1f , 0);
    }

    async void AddFlashMaterial()
    {
        flashMaterial_ = await Addressables.LoadAssetAsync<Material>("Assets/Materials/FlashMaterial.mat").Task;
    }

    void Flash()
    {
        if(flashRoutine_ != null)
        {
            StopCoroutine(flashRoutine_);
        }
        flashRoutine_ = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer_.material = flashMaterial_;
        Color newColor;
        ColorUtility.TryParseHtmlString("#c2dbe0", out newColor);
        flashMaterial_.color = newColor;

        yield return new WaitForSeconds(flashDuration_);
        spriteRenderer_.material = originalMaterial_;
        flashRoutine_ = null;
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilGetHurt -= Flash;
    }
}
