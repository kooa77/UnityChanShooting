using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetState : State
{
    override public void Start()
    {
        _character.CharacterModel.GetComponent<Animator>().SetTrigger("idle");
        _character.FindTarget();
    }

    override public void Update()
    {
        if(null != _character.GetTarget())
        {
            _character.Look(_character.GetTarget());
            _character.ChangeState(Player.eState.ATTACK);
        }
        else
        {
            _character.ChangeState(Player.eState.IDLE);
        }
    }
}

