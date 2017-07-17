using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyControlClip : PlayableAsset, ITimelineClipAsset
{
    public EnemyControlBehaviour template = new EnemyControlBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<EnemyControlBehaviour>.Create (graph, template);
		EnemyControlBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}
