using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EnemyMovementClip : PlayableAsset, ITimelineClipAsset
{
    public EnemyMovementBehaviour template = new EnemyMovementBehaviour ();
    public ExposedReference<BezierSpline> path;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<EnemyMovementBehaviour>.Create (graph, template);
        EnemyMovementBehaviour clone = playable.GetBehaviour ();
        clone.path = path.Resolve (graph.GetResolver ());
        return playable;
    }
}
