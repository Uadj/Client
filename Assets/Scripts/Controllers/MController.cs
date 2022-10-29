using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MController : CController
{

    protected override void Init()
    {
        base.Init();
        _State = _CreatureState.Idle;
        _Dir = _MoveDir.None;
    }
    protected override void UpdateController()
    {
        //GetDirInput();
        base.UpdateController();
    }
    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _Dir = _MoveDir.Up;
            //transform.position += Vector3.up * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _Dir = _MoveDir.Down;
            //transform.position += Vector3.down * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _Dir = _MoveDir.Left;
            //transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _Dir = _MoveDir.Right;
            //transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        else
        {
            _Dir = _MoveDir.None;
        }
    }
}
