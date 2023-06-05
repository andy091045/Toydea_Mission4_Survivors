using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class SmallWeaponAndItemUI : MonoBehaviour
{
    public Image[] ItemUIGroup;
    public List<int> ItemLevelGroup = new List<int>();
    public Image[] WeaponUIGroup;
    public List<int> WeaponLevelGroup = new List<int>();

    public GameObject[] ItemGameObjectGroup;
    public GameObject[] WeaponGameObjectGroup;

    DataManager dataManager_;
    UnityData unityData_;

    AssetReference target_ = new AssetReference();

    void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        dataManager_ = GameContainer.Get<DataManager>();
        EventManager.OccurChooseItem += UpdateItemUI;
        EventManager.OccurChooseWeapon += UpdateWeaponUI;
    }

    async void UpdateItemUI()
    {
        int uiCount = unityData_.HoldItems.Count;
        var references = new List<AssetReference>();
        ItemLevelGroup.Clear();

        for (int i = 0; i < uiCount; i++)
        {
            for (int j = 0; j < dataManager_.dataGroup.itemsData.Count; j++)
            {
                if (unityData_.HoldItems[i] == dataManager_.dataGroup.itemsData[j].ItemName)
                {
                    target_ = new AssetReferenceSprite(dataManager_.dataGroup.itemsData[j].UIPath);
                    target_.SubObjectName = dataManager_.dataGroup.itemsData[j].UIName;
                    ItemLevelGroup.Add(dataManager_.dataGroup.itemsData[j].NowItemLevel);
                    references.Add(target_);
                }
            }            
        }

        var sprites = await GetSprites(references);

        for (int i = 0; i < uiCount; i++)
        {
            ObjectLevelUI(ItemGameObjectGroup[i], ItemLevelGroup[i]);
            ItemUIGroup[i].sprite = sprites[i];
        }
    }

    async void UpdateWeaponUI()
    {        
        int uiCount = unityData_.HoldWeapons.Count;
        var references = new List<AssetReference>();
        WeaponLevelGroup.Clear();

        for (int i = 0; i < uiCount; i++)
        {
            for (int j = 0; j < dataManager_.dataGroup.weaponsData.Count; j++)
            {
                if (unityData_.HoldWeapons[i] == dataManager_.dataGroup.weaponsData[j].WeaponName)
                {
                    target_ = new AssetReferenceSprite(dataManager_.dataGroup.weaponsData[j].UIPath);
                    target_.SubObjectName = dataManager_.dataGroup.weaponsData[j].UIName;
                    WeaponLevelGroup.Add(dataManager_.dataGroup.weaponsData[j].NowWeaponLevel);
                    references.Add(target_);
                }
            }
        }

        var sprites = await GetSprites(references);

        for (int i = 0; i < uiCount; i++)
        {
            ObjectLevelUI(WeaponGameObjectGroup[i], WeaponLevelGroup[i]);
            WeaponUIGroup[i].sprite = sprites[i];
        }
    }

    void ObjectLevelUI(GameObject obj, int length)
    {        
        for (int i = 0; i < length; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public async Task<List<Sprite>> GetSprites(List<AssetReference> targets)
    {
        List<Sprite> sprites = new List<Sprite>();
        foreach (var item in targets)
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(item);
            await handle.Task;
            sprites.Add(handle.Result);
        }
        return sprites;
        //ImageGroup[imageNumber].sprite = handle.Result;
    }

    private void OnDestroy()
    {
        EventManager.OccurChooseItem -= UpdateItemUI;
        EventManager.OccurChooseWeapon -= UpdateWeaponUI;
    }
}
