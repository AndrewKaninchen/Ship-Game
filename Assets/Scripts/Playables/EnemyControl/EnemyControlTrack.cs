using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0f, 0f)]
[TrackClipType(typeof(EnemyControlClip))]
[TrackBindingType(typeof(EnemyShipController))]
public class EnemyControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
		var playable = ScriptPlayable<EnemyControlMixerBehaviour>.Create(graph, inputCount);
		return playable;
    }
}
