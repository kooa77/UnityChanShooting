using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingBullet : BulletItem
{
    float _rotationSpeed = 2.0f;

	void Update ()
    {
        if(null != _target)
        {
            Vector3 targetPosition = _target.transform.position;
            targetPosition.y = transform.position.y;

            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }

        Vector3 moveOffset = _moveSpeed * Vector3.forward;
        transform.position += ((transform.rotation * moveOffset) * Time.deltaTime);
    }
}
