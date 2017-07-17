using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyMovementBehaviour))]
public class EnemyMovementDrawer : PropertyDrawer
{
	SerializedProperty p;


	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 1;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
		p = property;
        SerializedProperty positionOverTimeProp = property.FindPropertyRelative("positionOverTime");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, positionOverTimeProp);
    }
}