using Codice.CM.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ChooseItemManager : MonoBehaviour
{   
    public GameObject[] ButtonGameObjects;
    [SerializeField] Button[] buttons = new Button[3];

    //class t
    //{
    //    Button b;
    //    Image i;
    //    public void SetImage(Sprite s) => i.sprite = s;
    //}

    //List<t> buttonList_;

    public Image[] ImageGroup;

    UnityData unityData_;
    DataManager dataManager_;

    List<string> randomList_ = new List<string>();
    ObjectType objectType;
    List<ObjectType> objectTypes = new List<ObjectType>();

    AssetReference target_ = new AssetReference();


    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_.DevilLevel.OnValueChanged += SetButton;

        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            buttons[i] = ButtonGameObjects[i].GetComponent<Button>();
        }

        CloseButton();
        ResetRandomList();
        SetButton(0);        
    }

    void CloseButton()
    {
        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            ButtonGameObjects[i].SetActive(false);
        }
    }

    void ResetRandomList()
    {
        randomList_.Clear();
        objectTypes.Clear();       

        for (int i = 0; i < dataManager_.dataGroup.itemsData.Count; i++)
        {
            if (dataManager_.dataGroup.itemsData[i].NowItemLevel < 4)
            {
                randomList_.Add(dataManager_.dataGroup.itemsData[i].ItemName);
            }
        }

        for (int i = 0; i < dataManager_.dataGroup.weaponsData.Count; i++)
        {
            if (dataManager_.dataGroup.weaponsData[i].NowWeaponLevel < 4)
            {
                randomList_.Add(dataManager_.dataGroup.weaponsData[i].WeaponName);
            }
        }
    }


    async void SetButton(int level)
    {
        var references = new List<AssetReference>();
        //var objectTypes = new List<ObjectType>();

        for(int i = 0; i < ButtonGameObjects.Length; i++) {
            references.Add(GetRandomObjectInItemAndWeapon());
            objectTypes.Add(objectType);
        }

        var sprites = await GetSprites(references);

        for (int i = 0; i < 3; i++)
        {
            ImageGroup[i].sprite = sprites[i];
        }

        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            if (objectTypes[i].Type == "Item")
            {
                string name = objectTypes[i].ObjectName;
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(() => ChooseItem(name));
            }
            else
            {
                string name = objectTypes[i].ObjectName;
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(() => ChooseWeapon(name));
            }

            ButtonGameObjects[i].SetActive(true);
        }
        KeyInputManager.Instance.IsObjectCanMove = false;

        ResetRandomList();
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

    //void SetButtonImage(Image image, Sprite sprite)
    //{
    //    image.sprite = sprite;
    //}

    AssetReference GetRandomObjectInItemAndWeapon()
    {

        int selectedIndex = UnityEngine.Random.Range(0, randomList_.Count);
        string name = randomList_[selectedIndex];

        for (int i = 0; i < dataManager_.dataGroup.itemsData.Count; i++)
        {
            if (name == dataManager_.dataGroup.itemsData[i].ItemName)
            {
                target_ = new AssetReferenceSprite(dataManager_.dataGroup.itemsData[i].UIPath);
                target_.SubObjectName = dataManager_.dataGroup.itemsData[i].UIName;
                randomList_.RemoveAt(selectedIndex);
                objectType.Type = "Item";
                objectType.ObjectName = dataManager_.dataGroup.itemsData[i].ItemName;
                return target_;
            }
        }

        for (int i = 0; i < dataManager_.dataGroup.weaponsData.Count; i++)
        {
            if (name == dataManager_.dataGroup.weaponsData[i].WeaponName)
            {
                target_ = new AssetReferenceSprite(dataManager_.dataGroup.weaponsData[i].UIPath);
                target_.SubObjectName = dataManager_.dataGroup.weaponsData[i].UIName;
                randomList_.RemoveAt(selectedIndex);
                objectType.Type = "Weapon";
                objectType.ObjectName = dataManager_.dataGroup.weaponsData[i].WeaponName;
                return target_;
            }
        }
        return null;        
    }

    public void ChooseItem(string itemName)
    {        
        for (int i = 0;i < dataManager_.dataGroup.itemsData.Count; i++)
        {
            if (itemName == dataManager_.dataGroup.itemsData[i].ItemName)
            {
                dataManager_.dataGroup.itemsData[i].NowItemLevel++;

                if (!unityData_.HoldItems.Contains(itemName) && unityData_.HoldItems.Count < 4)
                {
                    unityData_.HoldItems.Add(itemName);
                }
                            
                EventManager.OccurChooseItem.Invoke();
            }            
        }
        KeyInputManager.Instance.IsObjectCanMove = true;
        CloseButton();
    }

    public void ChooseWeapon(string WeaponName)
    {
        Debug.LogWarning(WeaponName);
        for (int i = 0; i < dataManager_.dataGroup.weaponsData.Count; i++)
        {
            if (WeaponName == dataManager_.dataGroup.weaponsData[i].WeaponName)
            {
                dataManager_.dataGroup.weaponsData[i].NowWeaponLevel++;
                for (int j = 0; j < unityData_.HoldWeapons.Count; j++)
                {
                    if (unityData_.HoldWeapons[j] != WeaponName && unityData_.HoldWeapons.Count < 4)
                    {
                        unityData_.HoldWeapons.Add(WeaponName);
                    }
                }
                EventManager.OccurChooseWeapon.Invoke();
            }
        }
        KeyInputManager.Instance.IsObjectCanMove = true;
        CloseButton();
    }

    struct ObjectType
    {
        public string Type;
        public string ObjectName;
    }
}

