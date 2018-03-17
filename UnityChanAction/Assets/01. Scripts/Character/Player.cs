using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CharacterModel;


    // Use this for initialization
    void Start ()
    {
        InitItem();

        InitState();
        ChangeState(eState.IDLE);
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckMouseLock();
        UpdateInput();

        UpdateRotate();
        UpdateState();
	}


    // State

    public enum eState
    {
        IDLE,
        MOVE,
        ATTACK,
    }

    Dictionary<eState, State> _stateDic = new Dictionary<eState, State>();
    State _currentState;

    void InitState()
    {
        State idleState = new IdleState();
        State moveState = new MoveState();
        State attackState = new AttackState();

        idleState.Init(this);
        moveState.Init(this);
        attackState.Init(this);

        _stateDic.Add(eState.IDLE, idleState);
        _stateDic.Add(eState.MOVE, moveState);
        _stateDic.Add(eState.ATTACK, attackState);
    }

    public void ChangeState(eState nextState)
    {
        _currentState = _stateDic[nextState];
        _currentState.Start();
    }

    void UpdateState()
    {
        _currentState.Update();
    }
        

    // Input

    bool _mouseLock = true;

    void CheckMouseLock()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _mouseLock = !_mouseLock;
        }

        if (_mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public enum eInputDirection
    {
        NONE, FRONT, BACK, LEFT, RIGHT
    }
    eInputDirection _inputVerticalDirection = eInputDirection.NONE;
    eInputDirection _inputHorizontalDirection = eInputDirection.NONE;
    eInputDirection _inputAniDirection = eInputDirection.NONE;

    void UpdateInput()
    {
        _inputVerticalDirection = eInputDirection.NONE;
        _inputHorizontalDirection = eInputDirection.NONE;
        _inputAniDirection = eInputDirection.NONE;

        if (Input.GetKey(KeyCode.W))
        {
            _inputVerticalDirection = eInputDirection.FRONT;
            _inputAniDirection = eInputDirection.FRONT;
        }        
        if (Input.GetKey(KeyCode.S))
        {
            _inputVerticalDirection = eInputDirection.BACK;
            _inputAniDirection = eInputDirection.BACK;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _inputHorizontalDirection = eInputDirection.LEFT;
            _inputAniDirection = eInputDirection.LEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _inputHorizontalDirection = eInputDirection.RIGHT;
            _inputAniDirection = eInputDirection.RIGHT;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ChangeState(eState.ATTACK);
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            ChangeState(eState.IDLE);
        }
    }

    public eInputDirection GetInputVerticalDirection()
    {
        return _inputVerticalDirection;
    }

    public eInputDirection GetInputHorizontalDirection()
    {
        return _inputHorizontalDirection;
    }

    public eInputDirection GetAniDirection()
    {
        return _inputAniDirection;
    }

    // Rotate

    float _rotationY = 0.0f;

    void UpdateRotate()
    {
        if (false == _mouseLock)
            return;

        float rateSpeed = 360.0f;
        float addRotationY = Input.GetAxis("Mouse X") * rateSpeed;
        _rotationY += (addRotationY * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0.0f, _rotationY, 0.0f);
    }


    // Move

    public void UpdateMove()
    {
        Vector3 addPosition = Vector3.zero;

        switch(_inputVerticalDirection)
        {
            case eInputDirection.FRONT:
                addPosition.z = MoveOffset(10.0f);
                break;
            case eInputDirection.BACK:
                addPosition.z = MoveOffset(-5.0f);
                break;
        }

        switch (_inputHorizontalDirection)
        {
            case eInputDirection.LEFT:
                addPosition.x = MoveOffset(-6.0f);
                break;
            case eInputDirection.RIGHT:
                addPosition.x = MoveOffset(6.0f);
                break;
        }

        transform.position += (transform.rotation * addPosition);
    }

    float MoveOffset(float moveSpeed)
    {
        return (moveSpeed * Time.deltaTime);
    }


    // Attack

    public void Shot()
    {
        Quaternion fireRotation = transform.rotation;
        _gun.Fire(fireRotation);
    }

    public float GetShotSpeed()
    {
        return _gun.GetShotSpeed();
    }


    // Item

    public GameObject GunObject;
    public GameObject BulletPrefab;

    GunItem _gun;

    void InitItem()
    {
        _gun = GunObject.GetComponent<GunItem>();
        _gun.SetBullet(BulletPrefab);
    }


    // Animation

    public AnimationModule GetAnimationModule()
    {
        return CharacterModel.GetComponent<AnimationModule>();
    }
}
