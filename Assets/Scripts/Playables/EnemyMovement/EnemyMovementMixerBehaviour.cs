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
				var position = input.path.GetPoint(input.positionOverTime.Evaluate(normalizedTime));
				trackBinding.transform.position = position;
			}      
        }
    }
}
