using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject CharacterModel;

    void Start ()
    {
        Init();
    }
	
	void Update ()
    {
        UpdateProcess();
    }

    public void Init()
    {
        InitGroupType();
        InitItem();
        InitState();
        ChangeState(eState.IDLE);
    }

    // GroupType

    public enum eGroupType
    {
        PLAYER,
        ENEMY,
    }

    protected eGroupType _groupType;

    virtual protected void InitGroupType()
    {
        _groupType = eGroupType.PLAYER;
    }

    public eGroupType GetGroupType()
    {
        return _groupType;
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
        TAKE_OFF,
        LANDING,
        ATTACK,
        FIND_TARGET,
    }

    protected Dictionary<eState, State> _stateDic = new Dictionary<eState, State>();
    protected State _currentState;

    virtual protected void InitState()
    {
        State idleState = new IdleState();
        State moveState = new MoveState();
        State takeOffState = new TakeOffState();
        State landingState = new LandingState();
        State attackState = new AttackState();
        State findTargetState = new FindTargetState();

        idleState.Init(this);
        moveState.Init(this);
        takeOffState.Init(this);
        landingState.Init(this);
        attackState.Init(this);
        findTargetState.Init(this);

        _stateDic.Add(eState.IDLE, idleState);
        _stateDic.Add(eState.MOVE, moveState);
        _stateDic.Add(eState.TAKE_OFF, takeOffState);
        _stateDic.Add(eState.LANDING, landingState);
        _stateDic.Add(eState.ATTACK, attackState);
        _stateDic.Add(eState.FIND_TARGET, findTargetState);
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

    protected bool _isAir = false;

    public void SetAir(bool isAir)
    {
        _isAir = isAir;
    }

    public void UpdateMove()
    {
        Vector3 addPosition = Vector3.zero;

        switch (_inputVerticalDirection)
        {
            case eInputDirection.FRONT:
                addPosition.z = MoveOffset(4.0f);
                break;
            case eInputDirection.BACK:
                addPosition.z = MoveOffset(-2.0f);
                break;
        }

        switch (_inputHorizontalDirection)
        {
            case eInputDirection.LEFT:
                addPosition.x = MoveOffset(-4.0f);
                break;
            case eInputDirection.RIGHT:
                addPosition.x = MoveOffset(4.0f);
                break;
        }

        transform.position += (transform.rotation * addPosition);
    }

    public void UpdateTakeOff()
    {
        Vector3 takeOffPos = transform.position;
        takeOffPos.y = 6.0f;
        float upSpeed = 3.0f;
        transform.position = Vector3.Lerp(transform.position, takeOffPos, upSpeed * Time.deltaTime);
        if( Vector3.Distance(transform.position, takeOffPos) < 0.5f )
        {
            transform.position = takeOffPos;
            ChangeState(eState.IDLE);
        }
    }

    public void UpdateLanding()
    {
        Vector3 takeOffPos = transform.position;
        takeOffPos.y = 0.0f;
        float downSpeed = 6.0f;
        transform.position = Vector3.Lerp(transform.position, takeOffPos, downSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, takeOffPos) < 0.5f)
        {
            transform.position = takeOffPos;
            ChangeState(eState.IDLE);
        }
    }

    float MoveOffset(float moveSpeed)
    {
        return (moveSpeed * Time.deltaTime);
    }


    // Attack

    protected Character _target = null;

    public void Look(Character character)
    {
        Vector3 lookPos = character.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
        //transform.LookAt(character.transform);
    }

    public Character GetTarget()
    {
        return _target;
    }

    virtual public void FindTarget()
    {
        _target = null;        
    }

    public void Shot()
    {
        Quaternion fireRotation = transform.rotation;
        _gun.Fire(fireRotation, _target);
    }

    public float GetShotSpeed()
    {
        return _gun.GetShotSpeed();
    }


    // Item

    public GameObject GunObject;
    public GameObject BulletPrefab;

    protected GunItem _gun;

    virtual protected void InitItem()
    {
    }


    // Animation

    public AnimationModule GetAnimationModule()
    {
        return CharacterModel.GetComponent<AnimationModule>();
    }
}
