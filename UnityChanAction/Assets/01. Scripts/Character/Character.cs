using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject CharacterModel;

    void Start ()
    {
        InitItem();

        InitState();
        ChangeState(eState.IDLE);
    }
	
	void Update ()
    {
        UpdateProcess();
    }


    // Update

    virtual protected void UpdateProcess()
    {
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

    protected void InitState()
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
        if (null != _currentState)
        {
            _currentState.Stop();
        }

        _currentState = _stateDic[nextState];
        _currentState.Start();
    }

    protected void UpdateState()
    {
        _currentState.Update();
    }


    // Input

    public enum eInputDirection
    {
        NONE, FRONT, BACK, LEFT, RIGHT
    }
    protected eInputDirection _inputVerticalDirection = eInputDirection.NONE;
    protected eInputDirection _inputHorizontalDirection = eInputDirection.NONE;
    protected eInputDirection _inputAniDirection = eInputDirection.NONE;

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

    protected float _rotationY = 0.0f;

    virtual protected void UpdateRotate()
    {
        /*
        float rateSpeed = 360.0f;
        float addRotationY = Input.GetAxis("Mouse X") * rateSpeed;
        _rotationY += (addRotationY * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0.0f, _rotationY, 0.0f);
        */
    }


    // Move

    public void UpdateMove()
    {
        Vector3 addPosition = Vector3.zero;

        switch (_inputVerticalDirection)
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

    protected GunItem _gun;

    void InitItem()
    {
        //_gun = GunObject.AddComponent<GunItem>();
        //_gun = GunObject.AddComponent<NWayGunItem>();
        _gun = GunObject.AddComponent<SprialGunItem>();
        _gun.SetBullet(BulletPrefab);
    }


    // Animation

    public AnimationModule GetAnimationModule()
    {
        return CharacterModel.GetComponent<AnimationModule>();
    }
}
