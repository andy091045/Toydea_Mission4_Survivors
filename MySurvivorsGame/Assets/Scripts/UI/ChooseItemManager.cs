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

    [SerializeField] List<ButtonObject> ButtonObjects = new List<ButtonObject>();

    UnityData unityData_;
    DataManager dataManager_;

    List<string> randomList_ = new List<string>();
    ObjectType objectType_;
    List<ObjectType> objectTypes_ = new List<ObjectType>();

    AssetReference target_ = new AssetReference();

    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        dataManager_ = GameContainer.Get<DataManager>();
        unityData_.DevilLevel.OnValueChanged += SetButton;

        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            ButtonObject obj = new ButtonObject();
            obj.SetButtonObject(ButtonGameObjects[i]);
            ButtonObjects.Add(obj);
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
        objectTypes_.Clear();       

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
            objectTypes_.Add(objectType_);
        }

        var sprites = await GetSprites(references);

        for (int i = 0; i < 3; i++)
        {
            ButtonObjects[i].image.sprite = sprites[i];
        }

        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            if (objectTypes_[i].Type == "Item")
            {
                string name = objectTypes_[i].ObjectName;
                ButtonObjects[i].button.onClick.RemoveAllListeners();
                ButtonObjects[i].button.onClick.AddListener(() => ChooseItem(name));
            }
            else
            {
                string name = objectTypes_[i].ObjectName;
                ButtonObjects[i].button.onClick.RemoveAllListeners();
                ButtonObjects[i].button.onClick.AddListener(() => ChooseWeapon(name));
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
                objectType_.Type = "Item";
                objectType_.ObjectName = dataManager_.dataGroup.itemsData[i].ItemName;
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
                objectType_.Type = "Weapon";
                objectType_.ObjectName = dataManager_.dataGroup.weaponsData[i].WeaponName;
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

    class ButtonObject
    {
        public Button button;
        public Text text;
        public Image image;

        public void SetButtonObject(GameObject obj)
        {
            button = obj.GetComponent<Button>();
            text = obj.transform.GetChild(0).GetComponent<Text>();
            image = obj.transform.GetChild(1).GetComponent<Image>();
        }
    }
}

