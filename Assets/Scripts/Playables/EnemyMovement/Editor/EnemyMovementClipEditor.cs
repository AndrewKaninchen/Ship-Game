using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyMovementClip))]
public class EnemyMovementClipEditor : Editor {

	public override void OnInspectorGUI()
	{
		EnemyMovementClip clip = target as EnemyMovementClip;
		SerializedProperty template = serializedObject.FindProperty("template");

		#region Path
		SerializedProperty pathMode = template.FindPropertyRelative("pathMode");
		EditorGUILayout.PropertyField(pathMode);
		if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Linear)
		{
			SerializedProperty sp = template.FindPropertyRelative("startingPosition");
			SerializedProperty ep = template.FindPropertyRelative("endingPosition");
			EditorGUILayout.PropertyField(sp);
			EditorGUILayout.PropertyField(ep);
		}
		else if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Spline)
		{
			SerializedProperty path = serializedObject.FindProperty("path");
			EditorGUILayout.PropertyField(path);
		} 
		#endregion

		SerializedProperty velocityMode = template.FindPropertyRelative("velocityMode");
		EditorGUILayout.PropertyField(velocityMode);
		if (velocityMode.enumValueIndex == (int)EnemyMovementBehaviour.VelocityMode.Linear)
		{
			clip.template.positionOverTime = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		}
		else if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.VelocityMode.Curve)
		{
			SerializedProperty vel = template.FindPropertyRelative("positionOverTime");
			EditorGUILayout.PropertyField(vel);
		}
	}
}
