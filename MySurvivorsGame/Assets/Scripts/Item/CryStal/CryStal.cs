using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryStal : MonoBehaviour
{
    protected float expValue;
    protected DataManager dataManager;
    public UnityData unityData;

    bool canMove_ = false;

    protected virtual void Start()
    {
        dataManager = GameContainer.Get<DataManager>();
        unityData = GameContainer.Get<UnityData>();
    }

    void Update()
    {
        IsInAbsorbExpRange();
        TryMove();
    }

    void IsInAbsorbExpRange()
    {
        float distance = Vector3.Distance(transform.position, unityData.PlayerPos);
        if (distance < unityData.NowDevilData.AbsorbExpRange)
        {
            canMove_ = true;
        }
    }

    void TryMove()
    {
        if(canMove_)
        {
            var direction = unityData.PlayerPos - transform.position;

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
        unityData.EXP.Value += expValue;
        canMove_ = false;
        gameObject.SetActive(false);
    }
}
