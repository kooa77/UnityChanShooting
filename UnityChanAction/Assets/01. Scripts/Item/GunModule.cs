using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModule
{
    public int _wayCount = 10;
    public float _rotateY = 6.0f;

    protected GunItem _parentGunItem;

    public void Init(GunItem parentGunItem)
    {
        _parentGunItem = parentGunItem;
    }

    virtual public void Fire(Quaternion startRotation)
    {
        _parentGunItem.CreateBullet(startRotation);
    }
}
