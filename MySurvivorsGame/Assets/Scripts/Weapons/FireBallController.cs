using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : WeaponController
{
    protected override void Start()
    {
        WeaponName = "FireBall";
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        for (int i = 0; i < CurrentWeaponLevelData.Number; i++)
        {
            //Debug.LogWarning(Prefab.name);
            //GameObject spawnedMagicBall = Instantiate(Prefab);
            //spawnedMagicBall.transform.parent = transform;
            //spawnedMagicBall.transform.position = transform.position;
            //spawnedMagicBall.GetComponent<FireBallBehaviour>().DirectionChecker(CurrentWeaponLevelData.Number, i);
            //spawnedMagicBall.GetComponent<FireBallBehaviour>().weaponLevelData = CurrentWeaponLevelData;
            CreateFireBall(i);
        }
    }

    private void CreateFireBall(int i)
    {
        float angle = 360f / CurrentWeaponLevelData.Number;        
        Vector3 spawnPosition = GetPositionOnCircle(angle * i, 2);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle * i - 90f);
        GameObject fireBall = Instantiate(Prefab, spawnPosition, rotation);
        fireBall.transform.parent = transform;
        //fireBall.transform.position = transform.position;
        fireBall.GetComponent<FireBallBehaviour>().weaponLevelData = CurrentWeaponLevelData;
    }

    private Vector3 GetPositionOnCircle(float angle, float radius)
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = unityData.PlayerPos.x + radius * Mathf.Cos(radian);
        float y = unityData.PlayerPos.y + radius * Mathf.Sin(radian);
        float z = unityData.PlayerPos.z;
        return new Vector3(x, y, z);
    }
}