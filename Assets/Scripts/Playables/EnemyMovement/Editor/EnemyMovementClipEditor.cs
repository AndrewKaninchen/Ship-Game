using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyMovementClip))]
public class EnemyMovementClipEditor : Editor {

	EnemyMovementClip clip;
	EnemyMovementBehaviour behaviour;
	SerializedProperty templateSO;
	SerializedProperty pathMode;
	SerializedProperty velocityMode;

	public override void OnInspectorGUI()
	{
		clip = target as EnemyMovementClip;
		templateSO = serializedObject.FindProperty("template");
		behaviour = clip.template;

		#region Path
		{
			#region Draw Box
			var style = new GUIStyle();
			style.border = new RectOffset(3, 3, 3, 3);
			var box = EditorGUILayout.BeginVertical(style);
			GUI.Box(box, ""); 
			#endregion

			pathMode = templateSO.FindPropertyRelative("pathMode");
			EditorGUILayout.PropertyField(pathMode);
			if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Linear)
			{
				SerializedProperty sp = templateSO.FindPropertyRelative("startingPosition");
				SerializedProperty ep = templateSO.FindPropertyRelative("endingPosition");
				EditorGUILayout.PropertyField(sp);
				EditorGUILayout.PropertyField(ep);
			}
			else if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Spline)
			{
				SerializedProperty path = serializedObject.FindProperty("path");
				EditorGUILayout.PropertyField(path);
			}

			EditorGUILayout.EndVertical();
		}
		#endregion

		EditorGUILayout.Space();

		#region Velocity
		{
			#region Draw Box
			var style = new GUIStyle();
			style.border = new RectOffset(3, 3, 3, 3);
			var box = EditorGUILayout.BeginVertical(style);
			GUI.Box(box, ""); 
			#endregion

			velocityMode = templateSO.FindPropertyRelative("velocityMode");
			EditorGUILayout.PropertyField(velocityMode);
			if (velocityMode.enumValueIndex == (int)EnemyMovementBehaviour.VelocityMode.Linear)
			{
				clip.template.positionOverTime = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			}
			else if (velocityMode.enumValueIndex == (int)EnemyMovementBehaviour.VelocityMode.Curve)
			{
				SerializedProperty vel = templateSO.FindPropertyRelative("positionOverTime");
				EditorGUILayout.PropertyField(vel);
			}

			EditorGUILayout.EndVertical();
		}
		#endregion

		serializedObject.ApplyModifiedProperties();

		//if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Linear)
		//{
		//	Handles.color = Color.green;
		//	Debug.Log("SP: " + behaviour.startingPosition);
		//	Debug.Log("EP: " + behaviour.endingPosition);
		//	behaviour.startingPosition = Handles.DoPositionHandle(behaviour.startingPosition, Quaternion.identity);
		//	behaviour.endingPosition = Handles.DoPositionHandle(behaviour.endingPosition, Quaternion.identity);
		//	Handles.DrawLine(behaviour.startingPosition, behaviour.endingPosition);
		//}
	}
}
