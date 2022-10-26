using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PController : MonoBehaviour
{
    public Grid _grid;
    public float _speed = 5.0f;
    bool _isMoving = false;
    Animator _animator;
    Vector3Int _cellPos = Vector3Int.zero;
    _MoveDir _dir = _MoveDir.None;
    // Start is called before the first frame update
    public _MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;
            switch(value)
            {
                case _MoveDir.Up:
                    _animator.Play("WALK_BACK");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case _MoveDir.Down:
                    _animator.Play("WALK_FRONT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case _MoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case _MoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case _MoveDir.None:
                    if(_dir == _MoveDir.Up)
                    {
                        _animator.Play("IDLE_BACK");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == _MoveDir.Down)
                    {
                        _animator.Play("IDLE_FRONT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == _MoveDir.Right)
                    {
                        _animator.Play("IDLE_RIGHT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == _MoveDir.Left)
                    {
                        _animator.Play("IDLE_RIGHT");
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    break;
            }
            _dir = value;
        }
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        Vector3 pos = _grid.CellToWorld(_cellPos)  + new Vector3(0.5f, 0.5f);
        transform.position = pos;
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
        Vector3 moveDir = destPos - transform.position;
        Debug.Log("destPos" + destPos.x + "y" + destPos.y);
        float dist = moveDir.magnitude;
        Debug.Log("Dist"+dist);
        Debug.Log("x"+destPos.x);
        Debug.Log("y"+destPos.y);
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            _isMoving = true;
        }
    }
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Dir = _MoveDir.Up;
            //transform.position += Vector3.up * Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = _MoveDir.Down;
            //transform.position += Vector3.down * Time.deltaTime * _speed;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            Dir = _MoveDir.Left;
            //transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Dir = _MoveDir.Right;
            //transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        else
        {
            Dir = _MoveDir.None;
        }
    }
}
