using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    protected Vector3 previousDirection;

    public WeaponData weaponData;
    public WeaponLevelData weaponLevelData;

    public UnityData unityData;

    AudioSource audioSource_;

    protected virtual void Awake()
    {
        unityData = GameContainer.Get<UnityData>();
    }

    protected virtual void Start()
    {        
        Destroy(gameObject, weaponLevelData.Duration);
        SetSE();
    }

    async void SetSE()
    {
        audioSource_ = gameObject.AddComponent<AudioSource>();
        AudioClip clip = await Addressables.LoadAssetAsync<AudioClip>(weaponData.GetHurtSoundPath).Task;
        audioSource_.clip = clip;
        audioSource_.volume *= 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            audioSource_.Play();
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(weaponLevelData.Hurt * unityData.NowDevilData.Attack);
        }
    }

    public void DirectionChecker(int weaponCount, int currentWeaponNumber)
    {
        float angleIncrement = 360f / weaponCount;
        float angle = currentWeaponNumber * angleIncrement;

        if (unityData.PlayerDir != Vector3.zero)
        {
            direction = unityData.PlayerDir;
        }
        else
        {
            direction = unityData.PreviousPlayerDir;
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
