using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0f, 0.5586205f)]
[TrackClipType(typeof(EnemyMovementClip))]
[TrackBindingType(typeof(EnemyShipController))]
public class EnemyMovementTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
		var playable = ScriptPlayable<EnemyMovementMixerBehaviour>.Create(graph, inputCount);
		return playable;
    }
}
