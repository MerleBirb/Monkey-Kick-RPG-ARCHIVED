//===== STAT PROPERTY DRAWER =====//
/*
5/16/21
Description:
- The property drawer for the CharacterStat class. 
- Improves workflow.

Author: Merlebirb
*/


using UnityEditor;
using UnityEngine;
using MonkeyKick.Character;

[CustomPropertyDrawer(typeof(CharacterStat))]
public class StatPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var rect = new Rect(position.position, Vector2.one * 20);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("BaseValue"), GUIContent.none);
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
