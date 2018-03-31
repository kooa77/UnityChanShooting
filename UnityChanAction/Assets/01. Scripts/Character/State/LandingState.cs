using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : State
{
    Player.eInputDirection _prevAniDirection;

    override public void Start()
    {
        _prevAniDirection = Player.eInputDirection.NONE;
        UpdateAnimation();
    }

    override public void Stop()
    {
        _character.SetAir(false);
    }

    override public void Update()
    {
        UpdateAnimation();
        _character.UpdateLanding();
    }

    override public void ChangeState(Character.eState nextState)
    {
        //_character.ChangeState(nextState);
        // 도착했으면
            // 체인지 스테이트 작동
    }

    void UpdateAnimation()
    {
        if (_prevAniDirection == _character.GetAniDirection())
            return;

        _prevAniDirection = _character.GetAniDirection();
        switch (_character.GetAniDirection())
        {
            case Player.eInputDirection.FRONT:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("movefront");
                break;
            case Player.eInputDirection.BACK:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("moveback");
                break;
            case Player.eInputDirection.RIGHT:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("moveright");
                break;
            case Player.eInputDirection.LEFT:
                _character.CharacterModel.GetComponent<Animator>().SetTrigger("moveleft");
                break;
        }
    }
}
