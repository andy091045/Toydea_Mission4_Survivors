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
    public float Damage;
    public float Speed;
    public float CooldownDuration;
    public int Pierce;
    float currentCooldown_;    

    public DataManager dataManager;
    public UnityData unityData;

    public string WeaponName = "";
    public WeaponData weaponData;

    protected virtual void Awake()
    {
        unityData = GameContainer.Get<UnityData>();
        dataManager = GameContainer.Get<DataManager>();
    }

    protected virtual void Start()
    {
        currentCooldown_ = CooldownDuration;
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
                break;
            }
        }

        if (weaponData.Level1Hurt == 0)
        {
            Debug.LogWarning("Q•s“ž" + WeaponName + "“IŽ‘—¿");
        }

        Prefab = await Addressables.LoadAssetAsync<GameObject>(weaponData.WeaponPrefabPath).Task;
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
