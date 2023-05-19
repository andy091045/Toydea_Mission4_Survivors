using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    DataManager data_;

    void Start()
    {
        data_ = GameContainer.Get<DataManager>();
    }

    public void ChooseDevil(string devilName)
    {
        data_.ChooseDevil(devilName);
    }
}
