using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemyMovementMixerBehaviour : PlayableBehaviour
{
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        EnemyShipController trackBinding = playerData as EnemyShipController;

        if (!trackBinding)
            return;

        int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<EnemyMovementBehaviour> inputPlayable = (ScriptPlayable<EnemyMovementBehaviour>)playable.GetInput(i);
            EnemyMovementBehaviour input = inputPlayable.GetBehaviour ();
            
            if(inputWeight == 1)
			{
				var normalizedTime = (float) (inputPlayable.GetTime() / inputPlayable.GetDuration());
				Vector3 position = new Vector3();
				if(input.pathMode == EnemyMovementBehaviour.PathMode.Linear)
				{
					position = Vector3.Lerp(input.startingPosition, input.endingPosition, input.positionOverTime.Evaluate(normalizedTime));
				}
				else if(input.pathMode == EnemyMovementBehaviour.PathMode.Spline)
				{
					position = input.splinePath.GetPoint(input.positionOverTime.Evaluate(normalizedTime));
					position += input.offset;
				}				
				trackBinding.transform.position = position;
			}      
        }
    }
}
