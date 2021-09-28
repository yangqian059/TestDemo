using UnityEngine;
using UnityEditor;

//CustomEditor 自定义编辑器
//描述了用于编辑器实时运行类型的一个编辑器类。
//注意：这是一个编辑器类，如果想使用它你需要把它放到工程目录下的Assets/Editor文件夹下。
//编辑器类在UnityEditor命名空间下。所以当使用C#脚本时，你需要在脚本前面加上 "using UnityEditor"引用。
[CustomEditor(typeof(CarouselFigure), true)]
[CanEditMultipleObjects]
public class CarouselFigureEditor : UnityEditor.UI.ScrollRectEditor
{
    SerializedProperty Toggles;
    SerializedProperty contentCount;
    //SerializedProperty _segements;

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
