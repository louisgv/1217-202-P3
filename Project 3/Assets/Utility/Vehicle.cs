﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vehicle base class.
/// Author LAB
/// Attached to: N/A
/// </summary>
public abstract class Vehicle : MonoBehaviour
{
	[SerializeField]
	private float mass = 1.0f;

	private Vector3 acceleration;

	private Vector3 direction;

	protected Transform seekingTarget;
	protected Transform fleeingTarget;
	protected Transform evasionTarget;

	[SerializeField, Range (0, 10f)]
	protected float maxForce = 5.0f;
	/// <summary>
	/// The max fleeing velocity.
	/// </summary>
	[SerializeField, Range (0, 10f)]
	protected float maxFleeingVelocity = 1;

	/// <summary>
	/// Gets the max fleeing velocity.
	/// </summary>
	/// <value>The max fleeing velocity.</value>
	public float MaxFleeingVelocity {
		get {
			return maxFleeingVelocity;
		}
	}

	[SerializeField, Range (0, 10f)]
	protected float fleeingForceScale = 2.0f;

	/// <summary>
	/// The max seeking velocity.
	/// </summary>
	[SerializeField, Range (0, 10f)]
	protected float maxSeekingVelocity = 1;

	/// <summary>
	/// Gets the max seeking velocity.
	/// </summary>
	/// <value>The max seeking velocity.</value>
	public float MaxSeekingVelocity {
		get {
			return maxSeekingVelocity;
		}
	}

	[SerializeField, Range (0, 10f)]
	protected float seekingForceScale = 2.0f;

	/// <summary>
	/// The max evasion velocity.
	/// </summary>
	[SerializeField, Range (0, 10f)]
	protected float maxEvasionVelocity = 1;

	/// <summary>
	/// Gets the max evasion velocity.
	/// </summary>
	/// <value>The max evasion velocity.</value>
	public float MaxEvasionVelocity {
		get {
			return maxEvasionVelocity;
		}
	}

	[SerializeField, Range (0, 10f)]
	protected float evasionForceScale = 2.0f;

	/// <summary>
	/// The max wandering velocity.
	/// </summary>
	[SerializeField, Range (0, 10f)]
	protected float maxWanderingVelocity = 1;

	/// <summary>
	/// Gets the max wandering velocity.
	/// </summary>
	/// <value>The max wandering velocity.</value>
	public float MaxWanderingVelocity {
		get {
			return maxWanderingVelocity;
		}
	}

	[SerializeField, Range (0, 10f)]
	protected float wanderingForceScale = 2.0f;

	/// <summary>
	/// Gets or sets the seeking target.
	/// </summary>
	/// <value>The seeking target.</value>
	public Transform SeekingTarget {
		get {
			return seekingTarget;
		}
		set {
			seekingTarget = value;
		}
	}

	/// <summary>
	/// Gets or sets the fleeing target.
	/// </summary>
	/// <value>The fleeing target.</value>
	public Transform FleeingTarget {
		get {
			return fleeingTarget;
		}
		set {
			fleeingTarget = value;
		}
	}

	/// <summary>
	/// Gets or sets the evasion target.
	/// </summary>
	/// <value>The evasion target.</value>
	public Transform EvasionTarget {
		get {
			return evasionTarget;
		}
		set {
			evasionTarget = value;
		}
	}

	/// <summary>
	/// Gets the direction.
	/// </summary>
	/// <value>The direction.</value>
	public Vector3 Direction {
		get {
			return direction;
		}
	}

	private Vector3 velocity;

	/// <summary>
	/// Gets the velocity.
	/// </summary>
	/// <value>The velocity.</value>
	public Vector3 Velocity {
		get {
			return velocity;
		}
	}

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	protected abstract Vector3 GetSteeringForce ();

	/// <summary>
	/// Applies ann add-on acceleration.
	/// </summary>
	/// <param name="addonAcceleration">Addon acceleration.</param>
	protected void ApplyAcceleration (Vector3 addonAcceleration)
	{
		acceleration += addonAcceleration;
	}

	/// <summary>
	/// Applies the force to our acceleartion.
	/// </summary>
	/// <param name="force">Force.</param>
	protected void ApplyForce (Vector3 force)
	{
		acceleration += force / mass;
	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	protected void Move ()
	{
		velocity += acceleration * Time.deltaTime;

		direction = velocity.normalized;

		transform.position += velocity * Time.deltaTime;
	}

	/// <summary>
	/// Reset variable that has small rate of change, e.g acceleration
	/// </summary>
	protected void Reset ()
	{
		acceleration = Vector3.zero;
	}

	// Update is called once per frame
	protected void LateUpdate ()
	{
		ApplyForce (GetSteeringForce ());
		
		Move ();

		Reset ();
	}
}