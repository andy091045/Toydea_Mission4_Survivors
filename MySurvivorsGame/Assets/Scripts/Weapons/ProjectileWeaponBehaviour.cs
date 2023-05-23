using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    protected Vector3 previousDirection;
    public float DestroyAfterSeconds;

    UnityData unityData_;

    protected virtual void Awake()
    {
        unityData_ = GameContainer.Get<UnityData>();
    }

    protected virtual void Start()
    {        
        Destroy(gameObject, DestroyAfterSeconds);
    }

    public void DirectionChecker()
    {
        if(unityData_.PlayerDir != Vector3.zero)
        {
            direction = unityData_.PlayerDir;
        }
        else
        {
            direction = unityData_.PreviousPlayerDir;
        }

        SetPrefabRotate();
    }

    void SetPrefabRotate()
    {
        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirx < 0 && diry == 0) // left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) // 
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) // up
        {
            scale.x = scale.x * -1;
        }
        else if (direction.x > 0 && direction.y > 0)
        {
            rotation.z = 0f;
        }
        else if(direction.x > 0 && direction.y < 0 )
        {
            rotation.z = -90f;
        }
        else if(direction.x < 0 && direction.y > 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
