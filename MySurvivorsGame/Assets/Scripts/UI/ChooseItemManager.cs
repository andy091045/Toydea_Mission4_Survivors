using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ChooseItemManager : MonoBehaviour
{   
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public Image[] ImageGroup;

    UnityData unityData_;
    DataManager dataManager_;

    List<string> itemRandomList_ = new List<string>();
    List<string> weaponRandomList_ = new List<string>();

    AssetReference target_ = new AssetReference();

    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_.DevilLevel.OnValueChanged += SetButton;

        CloseButton();
        ResetRandomList();
        SetButton(0);        
    }

    void CloseButton()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
    }

    void ResetRandomList()
    {
        itemRandomList_.Clear();
        weaponRandomList_.Clear();

        for (int i = 0; i < dataManager_.dataGroup.itemsData.Count; i++)
        {
            if (dataManager_.dataGroup.itemsData[i].NowItemLevel < 4)
            {
                itemRandomList_.Add(dataManager_.dataGroup.itemsData[i].ItemName);
            }
        }

        for (int i = 0; i < dataManager_.dataGroup.weaponsData.Count; i++)
        {
            if (dataManager_.dataGroup.weaponsData[i].NowWeaponLevel < 4)
            {
                weaponRandomList_.Add(dataManager_.dataGroup.weaponsData[i].WeaponName);
            }
        }
    }

    void SetButton(int level)
    {
        SetButtonImage(0, GetRandomObjectInItemAndWeapon());
        SetButtonImage(1, GetRandomObjectInItemAndWeapon());
        SetButtonImage(2, GetRandomObjectInItemAndWeapon());
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);        
        ResetRandomList();
    }

    public async void SetButtonImage(int imageNumber, AssetReference target)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(target);
        await handle.Task;

        ImageGroup[imageNumber].sprite = handle.Result;
    }

    AssetReference GetRandomObjectInItemAndWeapon()
    {
        int selectedIndex = UnityEngine.Random.Range(0, 2);
        if (selectedIndex == 0)
        {
            selectedIndex = UnityEngine.Random.Range(0, itemRandomList_.Count);
            string itemName = itemRandomList_[selectedIndex];

            for (int i = 0; i < dataManager_.dataGroup.itemsData.Count; i++)
            {
                if (itemName == dataManager_.dataGroup.itemsData[i].ItemName)
                {
                    target_ = new AssetReferenceSprite(dataManager_.dataGroup.itemsData[i].UIPath);
                    target_.SubObjectName = dataManager_.dataGroup.itemsData[i].UIName;
                    itemRandomList_.RemoveAt(selectedIndex);
                    return target_;
                }
            }
        }
        else
        {
            selectedIndex = UnityEngine.Random.Range(0, weaponRandomList_.Count);
            string weaponName = weaponRandomList_[selectedIndex];

            for (int i = 0; i < dataManager_.dataGroup.weaponsData.Count; i++)
            {
                if (weaponName == dataManager_.dataGroup.weaponsData[i].WeaponName)
                {
                    target_ = new AssetReferenceSprite(dataManager_.dataGroup.weaponsData[i].UIPath);
                    target_.SubObjectName = dataManager_.dataGroup.weaponsData[i].UIName;
                    weaponRandomList_.RemoveAt(selectedIndex);
                    return target_;
                }
            }
        }
        
        return null;        
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

