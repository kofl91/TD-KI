using UnityEngine;
using System.Collections;
using System;

public class WalkingState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        return input * motor.Speed;
    }
}
