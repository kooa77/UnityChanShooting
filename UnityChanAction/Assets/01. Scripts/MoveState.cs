using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    override public void Start()
    {
        _character.StartMoveState();
    }

    override public void Update()
    {
        _character.UpdateMoveState();
    }
}
