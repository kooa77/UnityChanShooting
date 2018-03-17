using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Player _character;

    public void Init(Player character)
    {
        _character = character;
    }

    virtual public void Start()
    {
    }

    virtual public void Update()
    {
    }
}
