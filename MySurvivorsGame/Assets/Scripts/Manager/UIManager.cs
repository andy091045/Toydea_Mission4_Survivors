using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager : MonoBehaviour
{
    public GameObject Canvas;
    public SceneType sceneType;

    DataManager data_;    

    void Start()
    {
        data_ = GameContainer.Get<DataManager>();        
        InstantiateUIComponent();
    }

    void InstantiateUIComponent()
    {
        switch (sceneType.ToString())
        {            
            case "ChooseDevils":                
                break;
            case "Battle":
                InstantiateMessageSysem();
                break;
            default: break;
        }        
    }

    void InstantiateMessageSysem()
    {
        GameObject messageSysem = new GameObject("MessageSystem");
        messageSysem.transform.parent = Canvas.transform;
        messageSysem.AddComponent<MessageSystem>();
    }

    //async void InstantiateEXPBar()
    //{
    //    GameObject prefabObj = await Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/UI/Exp_Bar.prefab").Task;
    //    GameObject devilObject = Instantiate(prefabObj);
    //    devilObject.transform.parent = Canvas.transform;
    //}

    public void ChooseDevil(string devilName)
    {
        data_.ChooseDevil(devilName);
    }    
}

public enum SceneType
{
    ChooseDevils, Battle
}
