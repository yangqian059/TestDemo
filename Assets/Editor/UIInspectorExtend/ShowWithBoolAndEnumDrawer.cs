using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowWithBoolAndEnumAttribute))]
public class ShowWithBoolAndEnumDrawer : PropertyDrawer
{
    private bool IsCanShow(SerializedProperty property)
    {
        bool flag;
        ShowWithBoolAndEnumAttribute a = attribute as ShowWithBoolAndEnumAttribute;
        int num = property.propertyPath.LastIndexOf(".");
        if (num < 0)
        {
            flag = property.serializedObject.FindProperty(a.boolName).boolValue;
        }
        else
        {
            flag = property.serializedObject.FindProperty(property.propertyPath.Substring(0, num + 1) + a.boolName).boolValue;
        }
        if (flag == a.showWhenEqualBool)
        {
            int curEnumIndex = 0;
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
                    return a.showWhenEqualEnum;
            }
            return !a.showWhenEqualEnum;
        }
        return false;
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
