using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyLifetimeClip : PlayableAsset, ITimelineClipAsset
{
    public EnemyLifetimeBehaviour template = new EnemyLifetimeBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<EnemyLifetimeBehaviour>.Create (graph, template);		
        return playable;
    }
}
