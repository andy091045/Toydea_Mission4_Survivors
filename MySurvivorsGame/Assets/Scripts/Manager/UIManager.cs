using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    DataManager data_;
    public GameObject Canvas;

    void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        InstantiateUIComponent();
    }

    void InstantiateUIComponent()
    {
        GameObject messageSysem = new GameObject("MessageSystem");
        messageSysem.transform.parent = Canvas.transform;
        messageSysem.AddComponent<MessageSystem>();
    }

    public void ChooseDevil(string devilName)
    {
        data_.ChooseDevil(devilName);
    }    
}
