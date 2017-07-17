using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyControlBehaviour))]
public class EnemyControlDrawer : PropertyDrawer
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
        SerializedProperty positionOverTimeProp = property.FindPropertyRelative("action");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, positionOverTimeProp);
    }
}
