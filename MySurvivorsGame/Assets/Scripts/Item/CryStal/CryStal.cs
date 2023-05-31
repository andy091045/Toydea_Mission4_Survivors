using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryStal : MonoBehaviour
{
    protected float expValue;
    protected DataManager dataManager;
    UnityData unityData_;

    bool canMove_ = false;

    protected virtual void Start()
    {
        dataManager = GameContainer.Get<DataManager>();
        unityData_ = GameContainer.Get<UnityData>();
    }

    void Update()
    {
        IsInAbsorbExpRange();
        TryMove();
    }

    void IsInAbsorbExpRange()
    {
        float distance = Vector3.Distance(transform.position, unityData_.PlayerPos);
        if (distance < unityData_.NowDevilData.AbsorbExpRange)
        {
            canMove_ = true;
        }
    }

    void TryMove()
    {
        if(canMove_)
        {
            var direction = unityData_.PlayerPos - transform.position;

            DevilTryGetEXP(direction);

            transform.Translate(direction.normalized * Time.deltaTime * 10, Space.World);
        }        
    }    

    void DevilTryGetEXP(Vector3 distance)
    {
        if (distance.magnitude <= 0.1f)
        {
            RemoveCrystal();
        }
    }

    void RemoveCrystal()
    {
        unityData_.EXP += expValue;
        EventManager.OccurDevilGetEXPStal.Invoke();
        canMove_ = false;
        gameObject.SetActive(false);
    }
}
