using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemyControlMixerBehaviour : PlayableBehaviour
{
	public TrackAsset track;
	public PlayableDirector director;
	EnemyShipController trackBinding;

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
		trackBinding = playerData as EnemyShipController;

		int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<EnemyControlBehaviour> inputPlayable = (ScriptPlayable<EnemyControlBehaviour>)playable.GetInput(i);
			EnemyControlBehaviour input = inputPlayable.GetBehaviour ();
            
            if(inputWeight == 1)
			{
				if(trackBinding) trackBinding.currentAction = input.action;
			}      
        }
    }
}
