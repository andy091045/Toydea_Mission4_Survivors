using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    DataManager dataReadStore_;
    UnityData unityData_;
    private void Awake()
    {
        dataReadStore_ = GameContainer.Get<DataManager>();
        unityData_ = GameContainer.Get<UnityData>();
        dataReadStore_.SetDataGroup();
        unityData_.DevilLevel.Value = 0;
        unityData_.EXP.Value = 0;
    }

    public void LoadScene(int i)
    {
        dataReadStore_.StoreDataGroup();
        SceneManager.LoadScene(i);
    }
}
