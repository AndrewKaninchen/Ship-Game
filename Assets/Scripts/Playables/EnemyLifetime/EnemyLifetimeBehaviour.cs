using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyLifetimeBehaviour : PlayableBehaviour
{
	public ShipStats template;

	public override void OnGraphStart(Playable playable)
	{

	}	
}
