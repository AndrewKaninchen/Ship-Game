using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyMovementBehaviour : PlayableBehaviour
{
    public BezierSpline path;
    public AnimationCurve positionOverTime = AnimationCurve.Linear(0, 0, 1, 1);

    public override void OnGraphStart (Playable playable)
    {
        
    }
}
