﻿using UnityEngine;
using System.Collections;

public abstract class BaseMotor : MonoBehaviour {

    protected CharacterController controller;
    protected BaseState state;
    protected Transform thisTransform;

    protected float baseSpeed = 2.0f;
    protected float baseGravity = 25.0f;

    public float Speed { get { return baseSpeed; } }
    public float Gravity { get { return baseGravity; } }

    public Vector3 MoveVector { set; get; }

    protected abstract void UpdateMotor();

    // Use this for initialization
    public virtual void Start () {
        controller = gameObject.AddComponent<CharacterController>();
        thisTransform = transform;

        state = gameObject.AddComponent<WalkingState>();
        state.Construct();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateMotor();
	}

    protected virtual void Move()
    {
        controller.Move(MoveVector * Time.deltaTime);
    }
}
