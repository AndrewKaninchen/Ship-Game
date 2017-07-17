using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemyControlMixerBehaviour : PlayableBehaviour
{
	public TrackAsset track;
	public PlayableDirector director;
	EnemyShipController trackBinding;
	// NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
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
				if(input.action == EnemyAction.Spawn && trackBinding == null)
				{
					var s = GameObject.Instantiate(input.prefab).GetComponent<EnemyShipController>();
					foreach (TrackAsset t in track.GetGroup().GetChildTracks())
					{
						director.SetGenericBinding(t, s);			
					}
				}
				if(input.action == EnemyAction.Destroy)
				{
					if(trackBinding) GameObject.DestroyImmediate(trackBinding.gameObject);
				}

				if(trackBinding) trackBinding.currentAction = input.action;
			}      
        }
    }
}
