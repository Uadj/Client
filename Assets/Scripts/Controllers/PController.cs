using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PController : MonoBehaviour
{
    public Grid _grid;
    public float _speed = 5.0f;
    bool _isMoving = false;
    Vector3Int _cellPos = Vector3Int.zero;
    _MoveDir _dir = _MoveDir.None;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = _grid.CellToWorld(_cellPos)  + new Vector3(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirInput();
        UpdatePosition();
        UpdateIsMoving();
        
    }
    void UpdateIsMoving()
    {
        if (_isMoving == false)
        {
            switch (_dir)
            {
                case _MoveDir.Up:
                    _cellPos += Vector3Int.up;
                    _isMoving = true;
                    break;
                case _MoveDir.Down:
                    _cellPos += Vector3Int.down;
                    _isMoving = true;
                    break;
                case _MoveDir.Left:
                    _cellPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case _MoveDir.Right:
                    _cellPos += Vector3Int.right;
                    _isMoving = true;
                    break;

            }
        }
    }
    void UpdatePosition()
    {
        if(_isMoving == false)
        {
            return;
        }
        Vector3 destPos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        Vector3 _MoveDir = destPos - transform.position;

        float dist = _MoveDir.magnitude;
        if(dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += _MoveDir.normalized * _speed * Time.deltaTime;
            _isMoving = true;
        }
    }
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _dir = _MoveDir.Up;
            //transform.position += Vector3.up * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _dir = _MoveDir.Down;
            //transform.position += Vector3.down * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _dir = _MoveDir.Left;
            //transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _dir = _MoveDir.Right;
            //transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
