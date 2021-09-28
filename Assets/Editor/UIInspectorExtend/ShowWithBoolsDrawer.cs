using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowWithBoolsAttribute))]
public class ShowWithBoolsDrawer : PropertyDrawer
{
    private bool IsCanShow(SerializedProperty property)
    {
        ShowWithBoolsAttribute a = attribute as ShowWithBoolsAttribute;
        int num = property.propertyPath.LastIndexOf(".");
        bool[] flagArray = new bool[a.boolNames.Length];
        if (num < 0)
        {
            for (int i = 0; i < flagArray.Length; i++)
            {
                flagArray[i] = property.serializedObject.FindProperty(a.boolNames[i]).boolValue;
            }
        }
        else
        {
            for (int i = 0; i < flagArray.Length; i++)
            {
                flagArray[i] = property.serializedObject.FindProperty(property.propertyPath.Substring(0, num + 1) + a.boolNames[i]).boolValue;
            }
        }
        for (int i = 0; i < flagArray.Length; i++)
        {
            if (flagArray[i] != a.showWhenEqual)
                return false;
        }
        return true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (this.IsCanShow(property))
        {
            return EditorGUI.GetPropertyHeight(property);
        }
        return -2f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (this.IsCanShow(property))
        {
            EditorGUI.PropertyField(position, property, true);
        }
    }
}
