using UnityEngine;
using System.Collections;

public abstract class BaseState : MonoBehaviour {

    protected BaseMotor motor;

    #region BaseState implementation

    public virtual void Construct()
    {
        motor = GetComponent<BaseMotor>();
    }

    public virtual void Destruct()
    {
        Destroy(this);
    }

    public virtual void Transition()
    {

    }

    #endregion

    public abstract Vector3 ProcessMotion(Vector3 input);

    public virtual Quaternion ProcessRotation (Vector3 input)
    {
        return transform.rotation;
    }
}
