using DataDefinition;
using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    DataManager dataReadStore_;
    UnityData unityData_;
    ObjectPoolGroup objectPoolGroup_;

    private void Awake()
    {        
        dataReadStore_ = GameContainer.Get<DataManager>();
        unityData_ = GameContainer.Get<UnityData>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        dataReadStore_.SetDataGroup();
    }

    public void LoadScene(int i)
    {
        KeyInputManager.Instance.IsObjectCanMove= true;
        dataReadStore_.StoreDataGroup();
        SceneManager.LoadScene(i);
    }

    private void OnDestroy()
    {
        ResetData();
    }

    private void ResetData()
    {
        unityData_.NowDevilData = new DevilData();
        unityData_.PlayerDir = Vector3.zero;
        unityData_.PreviousPlayerDir = Vector3.zero;
        unityData_.PlayerPos = Vector3.zero;
        unityData_.VillagersNumber = 0;
        unityData_.WarriorsNumber = 0;
        unityData_.HoldItems.Clear();
        unityData_.HoldWeapons.Clear();
        unityData_.IsInNirvanaTime.Value = false;
        unityData_.EXP.Value = 0;
        unityData_.DevilLevel.Value = 0;
        unityData_.TotalDeadCount.Value = 0;

        objectPoolGroup_.NPCPools.Clear();
        objectPoolGroup_.CrystalPools.Clear();
    }
}
