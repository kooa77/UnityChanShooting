using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWayGunModule : GunModule
{
    override public void Fire(Quaternion startRotation)
    {
        Quaternion shotRotation = startRotation;
        float yRotOffset = 10.0f;

        float yRot = -(_wayCount / 2) * yRotOffset;

        for (int i = 0; i < _wayCount; i++)
        {
            shotRotation = startRotation * Quaternion.Euler(0.0f, yRot + (yRotOffset * i), 0.0f);
            _parentGunItem.CreateBullet(shotRotation);
        }
    }
}