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
		var mixer = ScriptPlayable<EnemyControlMixerBehaviour>.Create(graph, inputCount);
		mixer.GetBehaviour().track = this;
		mixer.GetBehaviour().director = go.GetComponent<PlayableDirector>();
		return mixer;
    }
}
