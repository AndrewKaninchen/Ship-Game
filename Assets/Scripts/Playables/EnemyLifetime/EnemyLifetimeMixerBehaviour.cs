using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemyLifetimeMixerBehaviour : PlayableBehaviour
{
	public TrackAsset track;
	public PlayableDirector director;
	private EnemyShipController instance;

	private bool hasSpawned = false;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		hasSpawned = false;
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		hasSpawned = false;
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
		if (!instance)
			instance = playerData as EnemyShipController;

		int inputCount = playable.GetInputCount ();

		//Loooking through all clips to see if one is running
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<EnemyLifetimeBehaviour> inputPlayable = (ScriptPlayable<EnemyLifetimeBehaviour>)playable.GetInput(i);
			EnemyLifetimeBehaviour input = inputPlayable.GetBehaviour ();
            
			//If a clip is running
            if(inputWeight == 1)
			{				
				if (!instance && !hasSpawned)
				{
					//Instantiate if there was no instance
					instance = GameObject.Instantiate(input.template.prefab).GetComponent<EnemyShipController>();
					hasSpawned = true;

					//Bind new instance to all tracks in the same group as this one
					foreach (TrackAsset t in track.GetGroup().GetChildTracks())
					{						
						director.SetGenericBinding(t, instance);
					}
				}
				//Guarantees that if a clip is running the last part of the code won't be reached
				return;
			}
        }

		//If this part is reached this means there is no clip running at this moment, so there should be no instance alive
		if(instance)
			GameObject.DestroyImmediate(instance.gameObject);
    }
}
