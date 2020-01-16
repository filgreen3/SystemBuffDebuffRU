using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Effect))]
[CanEditMultipleObjects]
public class EditorEffect : Editor {
    SerializedProperty nameProperty;

    SerializedProperty pointProperty;
    SerializedProperty percentProperty;
    SerializedProperty timeTarget;
    SerializedProperty timeAmount;
    SerializedProperty targetProperty;

    private bool openExtraProperty;
    private int targetPropertyIndex;
    Effect myEffect;

    void OnEnable () {
        myEffect = (Effect) target;
        targetPropertyIndex = (int) myEffect.TargetProperty % 2;
        nameProperty = serializedObject.FindProperty ("effectTag");
        pointProperty = serializedObject.FindProperty ("point");
        percentProperty = serializedObject.FindProperty ("usePercent");
        timeTarget = serializedObject.FindProperty ("timeType");
        timeAmount = serializedObject.FindProperty ("actionTime");
        targetProperty = serializedObject.FindProperty ("TargetProperty");
    }

    public override void OnInspectorGUI () {
        serializedObject.Update ();
        EditorGUILayout.PropertyField (nameProperty);
        EditorGUILayout.PropertyField (pointProperty);
        EditorGUILayout.PropertyField (timeTarget);
        if (myEffect.TimeType == TimeTypeEnum.Uniform)
            EditorGUILayout.PropertyField (percentProperty);

        DrawTimeTarget ();
        DrawTimeSetting ();

        serializedObject.ApplyModifiedProperties ();
    }
    private void DrawTimeTarget () {

        targetPropertyIndex = EditorGUILayout.Popup (targetPropertyIndex, optison);

        if (myEffect.TimeType == TimeTypeEnum.Stable)
            myEffect.TargetProperty = (TargetPropertyEnum) (targetPropertyIndex + 2);
        else
            myEffect.TargetProperty = (TargetPropertyEnum) targetPropertyIndex;

    }

    private void DrawTimeSetting () {
        if (myEffect.TimeType != TimeTypeEnum.Instant)
            EditorGUILayout.PropertyField (timeAmount);
        else
            EditorGUILayout.HelpBox ("No time property for instant effect", MessageType.Info);

    }
    private string[] optison = { "Healty", "Mana" };
}