using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataProcess;

public class PlayerTest : MonoBehaviour
{
    float speed;
    void Start()
    {
        var dataInit = DataContainer.Get<DataInit>();
        speed = dataInit.dataGroup.realTimePlayerData.Speed;

        KeyInputManager.Instance.onTowardRightMoveEvent.AddListener(MoveRight);
        KeyInputManager.Instance.onTowardLeftMoveEvent.AddListener(MoveLeft);
        KeyInputManager.Instance.onTowardUpMoveEvent.AddListener(MoveUp);
        KeyInputManager.Instance.onTowardDownMoveEvent.AddListener(MoveDown);
    }

    private void MoveRight()
    {
        transform.position += new Vector3( speed * Time.deltaTime, 0, 0);
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    private void MoveUp()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void MoveDown()
    {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}