using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

/// <summary>
/// Base script for all weapon controllers
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject Prefab;
    //public float Damage;
    //public float Speed;
    //public float CooldownDuration;
    //public int Pierce;

    public DataManager dataManager;
    public UnityData unityData;

    public string WeaponName = "";
    public WeaponData weaponData;
    public WeaponLevelData CurrentWeaponLevelData;
    public int WeaponLevel = 1;


    protected virtual void Awake()
    {
        unityData = GameContainer.Get<UnityData>();
        dataManager = GameContainer.Get<DataManager>();
    }

    protected virtual void Start()
    {        
        InstantiateWeapon();
        unityData.IsInNirvanaTime.OnValueChanged += ResetCooldown;        
    }

    private async void InstantiateWeapon()
    {
        for (int i = 0; i < dataManager.dataGroup.weaponsData.Count; i++)
        {
            if (dataManager.dataGroup.weaponsData[i].WeaponName == WeaponName)
            {
                weaponData = dataManager.dataGroup.weaponsData[i];
                SetCurrentWeaponLevelData();
                break;
            }
        }

        if (weaponData.SheetName == "")
        {
            Debug.LogWarning("Q•s“ž" + WeaponName + "“IŽ‘—¿");
        }
        Prefab = await Addressables.LoadAssetAsync<GameObject>(weaponData.WeaponPrefabPath).Task;
        CurrentWeaponLevelData.Cooldown *= unityData.NowDevilData.AttackCooldown;
    }

    protected virtual void Update()
    {
        if(CurrentWeaponLevelData != null)
        {
            CurrentWeaponLevelData.Cooldown -= Time.deltaTime;
            if (CurrentWeaponLevelData.Cooldown < 0)
            {
                Attack();
            }
        }        
    }

    protected virtual void Attack()
    {
        //CurrentWeaponLevelData.Cooldown = weaponData.LevelList[WeaponLevel-1].Clone().Cooldown * unityData.NowDevilData.AttackCooldown;        
        SetCurrentWeaponLevelData();
    }   

    public virtual void WeaponUpdate()
    {
        WeaponLevel++;
        SetCurrentWeaponLevelData();
    }
    void SetCurrentWeaponLevelData()
    {
        CurrentWeaponLevelData = weaponData.LevelList[weaponData.NowWeaponLevel-1].Clone();
        CurrentWeaponLevelData.Cooldown *= unityData.NowDevilData.AttackCooldown;
    }

    void ResetCooldown(bool isUseNirvana)
    {
        if (isUseNirvana)
        {
            CurrentWeaponLevelData.Cooldown = 0;
            Prefab.GetComponent<Light2DBase>().enabled = true;
        }
        else
        {
            Prefab.GetComponent<Light2DBase>().enabled = false;
        }        
    }

    private void OnDestroy()
    {
        unityData.IsInNirvanaTime.OnValueChanged -= ResetCooldown;
        Prefab.GetComponent<Light2DBase>().enabled = false;
    }
}
