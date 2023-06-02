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

    List<string> randomList = new List<string>();

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
        randomList.Clear();
        if (unityData_.HoldItems.Count <= 0)
        {
            unityData_.HoldItems = dataManager_.dataGroup.itemsData;
        }

        for (int i = 0; i < unityData_.HoldItems.Count; i++)
        {
            if (unityData_.HoldItems[i].NowItemLevel != 4)
            {
                randomList.Add(unityData_.HoldItems[i].ItemName);
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
        AssetReference target = new AssetReference();
        int selectedIndex = UnityEngine.Random.Range(0, randomList.Count-1);
        string itemName = randomList[selectedIndex];

        for (int i = 0; i < unityData_.HoldItems.Count; i++)
        {
            if (itemName == unityData_.HoldItems[i].ItemName)
            {
                target = new AssetReferenceSprite(dataManager_.dataGroup.itemsData[i].UIPath);
                target.SubObjectName = dataManager_.dataGroup.itemsData[i].UIName;
                randomList.RemoveAt(selectedIndex);
                return target;
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

