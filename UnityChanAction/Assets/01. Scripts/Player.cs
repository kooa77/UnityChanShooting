using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CharacterModel;


    // Use this for initialization
    void Start ()
    {
        StartIdleState();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckMouseLock();

        UpdateRotate();
        UpdateState();
	}


    // State

    public enum eState
    {
        IDLE,
        MOVE,
    }
    eState _state = eState.IDLE;

    void UpdateState()
    {
        switch (_state)
        {
            case eState.IDLE:
                UpdateIdleState();
                break;
            case eState.MOVE:
                UpdateMoveState();
                break;
        }
    }

    void StartIdleState()
    {
        _state = eState.IDLE;
        CharacterModel.GetComponent<Animator>().SetTrigger("idle");
    }

    void UpdateIdleState()
    {
        Vector3 inputVertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        Vector3 inputHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if( 0.0f != inputVertical.z || 0.0f!= inputHorizontal.x )
        {
            StartMoveState();
        }
    }

    void StartMoveState()
    {
        _state = eState.MOVE;

        Vector3 inputVertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        if (0.0f < inputVertical.z)
        {
            CharacterModel.GetComponent<Animator>().SetTrigger("movefront");
        }
        else if (inputVertical.z < 0.0f)
        {
            CharacterModel.GetComponent<Animator>().SetTrigger("moveback");
        }
    }

    void UpdateMoveState()
    {
        Vector3 inputVertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        Vector3 inputHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if (0.0f == inputVertical.z && 0.0f == inputHorizontal.x)
        {
            StartIdleState();
            return;
        }

        UpdateMove();
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

    void UpdateMove()
    {
        Vector3 addPosition = Vector3.zero;

        Vector3 inputVertical = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        if( 0.0f < inputVertical.z )
        {
            addPosition.z = MoveOffset(5.0f);
        }
        else if(inputVertical.z < 0.0f)
        {
            addPosition.z = MoveOffset(-2.0f);
        }

        Vector3 inputHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if(0.0f < inputHorizontal.x)
        {
            addPosition.x = MoveOffset(3.0f);
        }
        else if( inputHorizontal.x < 0.0f )
        {
            addPosition.x = MoveOffset(-3.0f);
        }

        transform.position += (transform.rotation * addPosition);
    }

    float MoveOffset(float moveSpeed)
    {
        return (moveSpeed * Time.deltaTime);
    }
}
