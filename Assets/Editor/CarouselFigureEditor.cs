using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CarouselFigure), true)]
[CanEditMultipleObjects]
public class CarouselFigureEditor : UnityEditor.UI.ScrollRectEditor
{
    SerializedProperty Toggles;
    SerializedProperty contentCount;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Setup the SerializedProperties.
        Toggles = serializedObject.FindProperty("toggleGroup");
        contentCount = serializedObject.FindProperty("contentCount");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();
        // Show the custom GUI controls.--显示自定义GUI控件
        //EditorGUILayout.Slider(Toggles, 0, 1, new GUIContent("ToogleGroup"));
        EditorGUILayout.PropertyField(contentCount);
        EditorGUILayout.PropertyField(Toggles);
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
