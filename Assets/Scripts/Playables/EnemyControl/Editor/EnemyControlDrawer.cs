using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyControlBehaviour))]
public class EnemyControlDrawer : PropertyDrawer
{
	//public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
 //   {
 //       int fieldCount = 1;
 //       return fieldCount * EditorGUIUtility.singleLineHeight;
 //   }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {		
        SerializedProperty actionProp = property.FindPropertyRelative("action");
		SerializedProperty prefabProp = property.FindPropertyRelative("prefab");

		//Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUILayout.PropertyField(actionProp);
	}
}
