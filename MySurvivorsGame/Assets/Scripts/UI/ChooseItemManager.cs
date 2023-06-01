using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseItemManager : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    UnityData unityData_;

    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        unityData_.DevilLevel.OnValueChanged += ButtonSet;
        CloseButton();
    }

    void CloseButton()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
    }

    void ButtonSet(int level)
    {
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);
    }

    public void ChooseItem(string itemName)
    {
        //unityData_.HoldItems.Add(itemsType);
        CloseButton();
    }

    public void ChooseWeapon(string WeaponName)
    {
        //unityData_.HoldWeapons.Add(weaponsType);
        CloseButton();
    }
}

