using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    override protected void UpdateProcess()
    {
        CheckMouseLock();
        UpdateInput();

        UpdateRotate();
        UpdateState();
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


    // Rotate

    override protected void UpdateRotate()
    {
        if (false == _mouseLock)
            return;

        float rateSpeed = 360.0f;
        float addRotationY = Input.GetAxis("Mouse X") * rateSpeed;
        _rotationY += (addRotationY * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0.0f, _rotationY, 0.0f);
    }
}
