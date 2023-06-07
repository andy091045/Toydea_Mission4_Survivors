using Codice.CM.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ChooseItemManager : MonoBehaviour
{   
    public GameObject[] ButtonGameObjects;

    [SerializeField] List<ButtonObject> ButtonObjects = new List<ButtonObject>();
    string text_ = "";

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

        for (int i = 0; i < unityData_.HoldItems.Count; i++)
        {
            for (int j = 0; j < dataManager_.dataGroup.itemsData.Count; j++)
            {
                if (unityData_.HoldItems[i] == dataManager_.dataGroup.itemsData[j].ItemName)
                {
                    if (dataManager_.dataGroup.itemsData[j].NowItemLevel < 4)
                    {
                        randomList_.Add(unityData_.HoldItems[i]);
                    }
                }
            }            
        }

        var remainList = new List<string>();
        for (int i = 0; i < dataManager_.dataGroup.itemsData.Count; i++)
        {
            if (dataManager_.dataGroup.itemsData[i].NowItemLevel < 4)
            {
                remainList.Add(dataManager_.dataGroup.itemsData[i].ItemName);
            }            
        }

        for (int i = 0; i < unityData_.HoldItems.Count; i++)
        {
            remainList.Remove(unityData_.HoldItems[i]);
        }   

        int[] numbers = new int[4 - unityData_.HoldItems.Count];

        HashSet<int> generatedNumbers = new HashSet<int>();
        for (int i = 0; i < numbers.Length; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = UnityEngine.Random.Range(0, remainList.Count);
            } while (generatedNumbers.Contains(randomNumber));

            numbers[i] = randomNumber;
            generatedNumbers.Add(randomNumber);
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            randomList_.Add(remainList[numbers[i]]);
        }

        //for (int i = 0; i < dataManager_.dataGroup.itemsData.Count; i++)
        //{
        //    if (dataManager_.dataGroup.itemsData[i].NowItemLevel < 4)
        //    {
        //        randomList_.Add(dataManager_.dataGroup.itemsData[i].ItemName);
        //    }
        //}

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
        int min = Mathf.Min(ButtonGameObjects.Length, randomList_.Count);
        if(min == 0)
        {
            return;
        }

        for (int i = 0; i < min; i++) {
            references.Add(GetRandomObjectInItemAndWeapon());
            ButtonObjects[i].text.text = text_;
            objectTypes_.Add(objectType_);
        }

        var sprites = await GetSprites(references);

        for (int i = 0; i < min; i++)
        {
            ButtonObjects[i].image.sprite = sprites[i];
        }

        for (int i = 0; i < min; i++)
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
    }

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
                text_ = dataManager_.dataGroup.itemsData[i].Description;
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
                text_ = dataManager_.dataGroup.weaponsData[i].Description;
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
        ResetRandomList();
    }

    public void ChooseWeapon(string WeaponName)
    {
        for (int i = 0; i < dataManager_.dataGroup.weaponsData.Count; i++)
        {
            if (WeaponName == dataManager_.dataGroup.weaponsData[i].WeaponName)
            {
                dataManager_.dataGroup.weaponsData[i].NowWeaponLevel++;

                if (!unityData_.HoldWeapons.Contains(WeaponName) && unityData_.HoldWeapons.Count < 4)
                {
                    unityData_.HoldWeapons.Add(WeaponName);
                }
                EventManager.OccurChooseWeapon.Invoke(dataManager_.dataGroup.weaponsData[i].ScriptName);
                //for (int j = 0; j < unityData_.HoldWeapons.Count; j++)
                //{
                //    Debug.LogWarning(j + ": " + unityData_.HoldWeapons[j]);
                //}    
            }
        }
        KeyInputManager.Instance.IsObjectCanMove = true;
        CloseButton();
        ResetRandomList();
    }

    struct ObjectType
    {
        public string Type;
        public string ObjectName;
    }

    class ButtonObject
    {
        public Button button;
        public TextMeshProUGUI text;
        public Image image;

        public void SetButtonObject(GameObject obj)
        {
            button = obj.GetComponent<Button>();
            text = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = "123";
            image = obj.transform.GetChild(1).GetComponent<Image>();
        }
    }

    private void OnDestroy()
    {
        unityData_.HoldWeapons.Clear();
    }
}

