using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateRotate();
        UpdateMove();
	}


    // Rotate

    float _rotationY = 0.0f;

    void UpdateRotate()
    {
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
