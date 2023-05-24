using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    protected Vector3 previousDirection;
    public WeaponLevelData weaponLevelData;

    UnityData unityData_;

    protected virtual void Awake()
    {
        unityData_ = GameContainer.Get<UnityData>();
    }

    protected virtual void Start()
    {        
        Destroy(gameObject, weaponLevelData.Duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(weaponLevelData.Hurt);
        }
    }

    public void DirectionChecker(int weaponCount, int currentWeaponNumber)
    {
        float angleIncrement = 360f / weaponCount;
        float angle = currentWeaponNumber * angleIncrement;

        if (unityData_.PlayerDir != Vector3.zero)
        {
            direction = unityData_.PlayerDir;
        }
        else
        {
            direction = unityData_.PreviousPlayerDir;
        }

        direction = Quaternion.Euler(0, 0, angle) * direction;

        SetPrefabRotate();
    }

    void SetPrefabRotate()
    {
        int dirx = Mathf.RoundToInt(direction.x);
        int diry = Mathf.RoundToInt(direction.y);

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx > 0 && diry == 0) // right
        {
            
        }
        else if (dirx == 0 && diry < 0) // down
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
