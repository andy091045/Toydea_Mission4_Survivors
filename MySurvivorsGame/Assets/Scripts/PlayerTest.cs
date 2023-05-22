using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataProcess;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTest : MonoBehaviour
{
    public Vector3 PlayerDir = Vector3.zero;
    [SerializeField] private float speed_;
    Rigidbody2D rgbd2d_;
    [SerializeField] private Vector3 movementVector;
    private void Awake()
    {
        rgbd2d_ = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
    }

    void Start()
    {
        var dataInit = GameContainer.Get<DataManager>();
        speed_ = dataInit.dataGroup.realTimePlayerData.Speed;

        KeyInputManager.Instance.onHorizontalMoveEvent.AddListener(GetHorizontalValue);
        KeyInputManager.Instance.onVerticalMoveEvent.AddListener(GetVerticalValue);

    }

    void GetHorizontalValue(float h)
    {
        movementVector.x = h;
    }

    void GetVerticalValue(float v)
    {
        movementVector.y = v;
    }

    void Update()
    {
        //var PoolGroup = GameContainer.Get<ObjectPoolGroup>();
        //var bullet = PoolGroup.objectPools_[0].Pool.GetInstance();    
        //bullet.transform.position = transform.position; 
        Move();
    }

    void Move()
    {
        PlayerDir = new Vector2(movementVector.x, movementVector.y).normalized;

        rgbd2d_.velocity = new Vector2(PlayerDir.x * speed_, PlayerDir.y * speed_);
    }
}
