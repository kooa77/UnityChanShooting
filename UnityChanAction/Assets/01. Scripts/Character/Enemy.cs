using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    override protected void InitGroupType()
    {
        _groupType = eGroupType.ENEMY;
    }

    override public void FindTarget()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Item

    override protected void InitItem()
    {
        _gun = GunObject.AddComponent<SprialGunItem>();
        _gun.InitGroupType(_groupType);
        _gun.SetBullet(BulletPrefab);
    }
}
