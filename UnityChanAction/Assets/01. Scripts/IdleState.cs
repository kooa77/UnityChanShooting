using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    override public void Start()
    {
        _character.StartIdleState();
    }

    override public void Update()
    {
        _character.UpdateIdleState();
    }
}
