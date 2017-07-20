using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0f, 0f)]
[TrackClipType(typeof(EnemyLifetimeClip))]
[TrackBindingType(typeof(EnemyShipController))]
public class EnemyLifetimeTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
		var mixer = ScriptPlayable<EnemyLifetimeMixerBehaviour>.Create(graph, inputCount);
		mixer.GetBehaviour().track = this;
		mixer.GetBehaviour().director = go.GetComponent<PlayableDirector>();
		return mixer;
    }
}
