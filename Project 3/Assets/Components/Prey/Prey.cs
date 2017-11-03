﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey: 
/// Default: Wandering around
/// If of close proximity to a predator: Run away
/// Attached to: Prey, Human
/// </summary>
public class Prey : SmartBoundedVehicle<PreyCollider>
{
	public Material glLineMaterial;

	private Color fleeingLineColor = Color.green;

	private List<Transform> fleeingTargets;

	private PredatorSystem targetPredatorSystem;

	/// <summary>
	/// Gets or sets the target predator system.
	/// </summary>
	/// <value>The target predator system.</value>
	public PredatorSystem TargetPredatorSystem {
		get {
			return targetPredatorSystem;
		}
		set {
			targetPredatorSystem = value;
		}
	}

	#region implemented abstract members of Vehicle

	protected override Vector3 GetTotalSteeringForce ()
	{
		var totalForce = Vector3.zero;

		// Get fleeing force:
		fleeingTargets = targetPredatorSystem.FindCloseProximityInstances (transform.position, fleeingParams.ThresholdSquared);

		foreach (var fleeingTarget in fleeingTargets) {
			var fleeingForce = SteeringForce.GetSteeringForce (this, fleeingTarget, SteeringMode.FLEEING);
			totalForce += fleeingForce;
		}

		totalForce *= fleeingParams.ForceScale;

		totalForce += GetObstacleEvadingForce () * evadingParams.ForceScale;

		totalForce += GetBoundingForce () * boundingParams.ForceScale;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);	
	}

	#endregion


	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	private void OnRenderObject ()
	{
		if (fleeingTargets == null) {
			return;
		}

		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		foreach (var fleeingTarget in fleeingTargets) {
			GL.Begin (GL.LINES);
			GL.Color (fleeingLineColor);
			GL.Vertex (transform.position);
			GL.Vertex (fleeingTarget.position);
			GL.End ();
			Debug.DrawLine (transform.position, fleeingTarget.position, fleeingLineColor);
		}
			
		GL.PopMatrix ();

	}
}
