using UnityEngine;
using System.Collections;

public class AIMotor : BaseMotor {

    private Vector3 destination = Vector3.zero;


    protected override void UpdateMotor()
    {
        MoveVector = PathDirection();

        // Send Input to filter
        MoveVector = state.ProcessMotion(MoveVector);

        // Move
        Move();
    }


    private Vector3 PathDirection()
    {
        if (destination == Vector3.zero)
            return destination;

        Vector3 dir = destination - thisTransform.position;

        dir.Set(dir.x, dir.y, dir.z);

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }
        return dir;
    }

    public void SetDestination(Transform t)
    {
        destination = t.position;
        //this.gameObject.transform.ro
        var targetPosition = t.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

    }

}
