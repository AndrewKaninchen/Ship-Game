using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyLifetimeBehaviour))]
public class EnemyLifetimeDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {        
		SerializedProperty prefabProp = property.FindPropertyRelative("prefab");
		EditorGUILayout.PropertyField(prefabProp);
	}
}
