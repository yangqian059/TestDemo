using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowWithBoolAttribute))]
public class ShowWithBoolDrawer : PropertyDrawer
{
    private bool IsCanShow(SerializedProperty property)
    {
        bool flag;
        ShowWithBoolAttribute a = attribute as ShowWithBoolAttribute;
        int num = property.propertyPath.LastIndexOf(".");
        if (num < 0)
        {
            flag = property.serializedObject.FindProperty(a.boolName).boolValue;
        }
        else
        {
            flag = property.serializedObject.FindProperty(property.propertyPath.Substring(0, num + 1) + a.boolName).boolValue;
        }
        return (flag == a.showWhenEqual);
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
