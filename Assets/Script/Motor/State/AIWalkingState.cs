using UnityEngine;
using System.Collections;

public class AIWalkingState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        return input * motor.Speed;
    }
}