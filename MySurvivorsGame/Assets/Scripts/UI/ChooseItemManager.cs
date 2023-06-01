using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseItemManager : MonoBehaviour
{
    [SerializeField] public Transform Button1;
    [SerializeField] public Transform Button2;
    [SerializeField] public Transform Button3;

    UnityData unityData_;

    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        Debug.Log("����1�ʒu: " + Button1.position);
        Debug.Log("����2�ʒu: " + Button2.position);
        Debug.Log("����3�ʒu: " + Button3.position);
    }

    public void ChooseItem(ItemsType itemsType)
    {
        unityData_.HoldItems.Add(itemsType);
    }

    public void ChooseWeapon(WeaponsType weaponsType)
    {
        unityData_.HoldWeapons.Add(weaponsType);
    }
}

