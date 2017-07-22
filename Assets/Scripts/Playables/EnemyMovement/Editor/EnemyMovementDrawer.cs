using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyMovementBehaviour))]
public class EnemyMovementDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
		//SerializedProperty positionOverTimeProp = property.FindPropertyRelative("positionOverTime");
		//EditorGUILayout.PropertyField(positionOverTimeProp);
    }
}
