using UnityEngine;
using System.Collections;
using System;

public class PlayerMotor : BaseMotor {


    protected override void UpdateMotor()
    {
        MoveVector = InputDirection();

        // Send Input to filter

        MoveVector = state.ProcessMotion(MoveVector);

        // Move
        Move();
    }

    private Vector3 InputDirection()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }
        return dir;
    }
}
