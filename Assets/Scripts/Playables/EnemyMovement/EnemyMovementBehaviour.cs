using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyMovementBehaviour : PlayableBehaviour
{	
	public enum PathMode
	{
		Spline,
		Linear
	}

	public enum VelocityMode
	{
		Curve,
		Linear
	}

	
	public Vector3 startingPosition, endingPosition;

	[Tooltip("The type of path the entity follow along this clip.")]
	public PathMode pathMode;

	[Tooltip("The type of velocity the entity will have along this clip.")]
	public VelocityMode velocityMode;

	public BezierSpline splinePath;
	public Vector3 offset;


	public AnimationCurve positionOverTime = AnimationCurve.Linear(0, 0, 1, 1);
}
