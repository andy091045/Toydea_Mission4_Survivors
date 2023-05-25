using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float ttl = 0.5f;
    private void Start()
    {
        Destroy(gameObject, ttl);
    }
}