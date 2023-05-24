using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
    }

    private async void InstantiateWeapon()
    {
        Debug.Log(dataManager.dataGroup.weaponsData);
        for (int i = 0; i < dataManager.dataGroup.weaponsData.Count; i++)
        {
            if (dataManager.dataGroup.weaponsData[i].WeaponName == WeaponName)
            {
                weaponData = dataManager.dataGroup.weaponsData[i];
                CurrentWeaponLevelData = weaponData.LevelList[0].Clone();
                break;
            }
        }

        if (weaponData.SheetName == "")
        {
            Debug.LogWarning("�Q�s��" + WeaponName + "�I����");
        }

        Prefab = await Addressables.LoadAssetAsync<GameObject>(weaponData.WeaponPrefabPath).Task;
    }

    protected virtual void Update()
    {
        CurrentWeaponLevelData.Cooldown -= Time.deltaTime;
        if(CurrentWeaponLevelData.Cooldown < 0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        CurrentWeaponLevelData.Cooldown = weaponData.LevelList[WeaponLevel-1].Clone().Cooldown;        
    }

    public virtual void WeaponUpdate()
    {
        WeaponLevel++;
        CurrentWeaponLevelData = weaponData.LevelList[WeaponLevel - 1].Clone();
    }
}
