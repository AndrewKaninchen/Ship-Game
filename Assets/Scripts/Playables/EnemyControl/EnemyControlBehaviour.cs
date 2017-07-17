using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyControlBehaviour : PlayableBehaviour
{
	public EnemyAction action;
	public GameObject prefab;

	public override void OnGraphStart(Playable playable)
	{

	}	
}
