using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ChooseItemManager : MonoBehaviour
{
    AssetReference target;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    UnityData unityData_;
    DataManager dataManager_;

    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_.DevilLevel.OnValueChanged += ButtonSet;
        CloseButton();

        Debug.Log(dataManager_.dataGroup.weaponsData[0].UIPath);
        ButtonSet(0);        
    }

    void CloseButton()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
    }

    async void ButtonSet(int level)
    {
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);

        target = new AssetReferenceSprite("Assets/ArtResources/MySelf/UI/HP_Bar.png");
        target.SubObjectName = "HP_Bar_0";
        //Addressables.LoadAssetAsync<SpriteAtlas>(target).Completed += Loaded;
        //Addressables.LoadAssetAsync<SpriteAtlas>("Assets/ArtResources/Weapons/Weapons Sprite Sheet.png").Completed += SpriteAtlasLoaded;
        var handle = Addressables.LoadAssetAsync<Sprite>(target);
         await handle.Task;

        Button1.GetComponent<Image>().sprite = handle.Result;
    }

    private void Loaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SpriteAtlas> handle)
    {
        var atlas = handle.Result;
        var sprite = atlas.GetSprite("Weapons Sprite Sheet_123");
        Button1.GetComponent<Image>().sprite = sprite;
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

