using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyMovementClip))]
public class EnemyMovementClipEditor : Editor {

	EnemyMovementClip clip;
	//EnemyMovementBehaviour behaviour;
	SerializedProperty templateSO;
	SerializedProperty pathMode;
	SerializedProperty velocityMode;
	SerializedProperty startingPosition;
	SerializedProperty endingPosition;


	private void OnEnable()
	{
		SceneView.onSceneGUIDelegate -= this.MySceneGUI;
		SceneView.onSceneGUIDelegate += this.MySceneGUI;

		clip = target as EnemyMovementClip;
		templateSO = serializedObject.FindProperty("template");
		//behaviour = clip.template;

		startingPosition = templateSO.FindPropertyRelative("startingPosition");
		endingPosition = templateSO.FindPropertyRelative("endingPosition");

	}

	private void OnDisable()
	{
		SceneView.onSceneGUIDelegate -= this.MySceneGUI;
	}

	public override void OnInspectorGUI()
	{		
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
				EditorGUILayout.PropertyField(startingPosition);
				EditorGUILayout.PropertyField(endingPosition);				
			}
			else if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Spline)
			{
				SerializedProperty path = serializedObject.FindProperty("path");
				SerializedProperty offset = templateSO.FindPropertyRelative("offset");
				EditorGUILayout.PropertyField(path);
				EditorGUILayout.PropertyField(offset);
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
	}

	private void MySceneGUI(SceneView sceneView)
	{
		if (pathMode.enumValueIndex == (int)EnemyMovementBehaviour.PathMode.Linear)
		{
			Handles.color = Color.green;
			startingPosition.vector3Value = Handles.DoPositionHandle(startingPosition.vector3Value, Quaternion.identity);
			endingPosition.vector3Value = Handles.DoPositionHandle(endingPosition.vector3Value, Quaternion.identity);
			Handles.DrawLine(startingPosition.vector3Value, endingPosition.vector3Value);

			serializedObject.ApplyModifiedProperties();
				OnInspectorGUI();
		}		
	}
}
