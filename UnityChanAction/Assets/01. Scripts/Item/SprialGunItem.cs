using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprialGunItem : GunItem
{
    Quaternion _shotRotation;
    bool _isFire = false;

    override public void Fire(Quaternion startRotation)
    {
        if (null != _bulletPrefab)
        {
            if(false == _isFire)
                _shotRotation = startRotation;
            else
                _shotRotation = _shotRotation * Quaternion.Euler(0.0f, 10.0f, 0.0f);
            _isFire = true;
            GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, transform.position, _shotRotation);
            bulletObject.transform.localScale = Vector3.one;
        }
    }
}
