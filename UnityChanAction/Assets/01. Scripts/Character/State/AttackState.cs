using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    bool _isShoot = false;

    override public void Start()
    {
        _isShoot = false;
        _shotTime = _character.GetShotSpeed();
        _character.GetAnimationModule().Play("attack", null, null, () =>
        {
            _isShoot = true;
        });
    }

    public override void Update()
    {
        base.Update();
        _character.CharacterModel.transform.localPosition = Vector3.zero;

        UpdateShoot();
    }


    // Shoot

    float _shotTime = 0.0f;

    void UpdateShoot()
    {
        if (false == _isShoot)
            return;

        if(_character.GetShotSpeed() <= _shotTime)
        {
            _shotTime = 0.0f;
            Shot();
        }
        _shotTime += Time.deltaTime;
    }

    void Shot()
    {
        _character.Shot();
    }
}
