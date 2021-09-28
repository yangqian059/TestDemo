using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowWithEnumAttribute))]
public class ShowWithEnumDrawer : PropertyDrawer
{
    private bool IsCanShow(SerializedProperty property)
    {
        int curEnumIndex;
        ShowWithEnumAttribute a = attribute as ShowWithEnumAttribute;
        int num = property.propertyPath.LastIndexOf(".");
        if (num < 0)
        {
            curEnumIndex = property.serializedObject.FindProperty(a.enumName).enumValueIndex;
        }
        else
        {
            curEnumIndex = property.serializedObject.FindProperty(property.propertyPath.Substring(0, num + 1) + a.enumName).enumValueIndex;
        }
        for (int i = 0; i < a.enumIndexs.Length; i++)
        {
            if (curEnumIndex == a.enumIndexs[i])
                return a.showWhenEqual;
        }

        return !a.showWhenEqual;
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
