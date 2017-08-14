using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FadeObjectOptions))]
public class FadeObjectOptionsInspector : Editor
{
    public override void OnInspectorGUI()
    {
        FadeObjectOptions fadeOption = target as FadeObjectOptions;

        EditorGUILayout.BeginHorizontal();
        fadeOption.OverrideFinalAlpha = EditorGUILayout.Toggle(
            new GUIContent("Override Alpha","Override the final alpha this object fades to, value should be between 0 and 1"), 
            fadeOption.OverrideFinalAlpha);
        if (fadeOption.OverrideFinalAlpha)
        {
            fadeOption.FinalAlpha = EditorGUILayout.FloatField(fadeOption.FinalAlpha);
            if (fadeOption.FinalAlpha < 0)
                fadeOption.FinalAlpha = 0;
            if (fadeOption.FinalAlpha > 1)
                fadeOption.FinalAlpha = 1;            
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        fadeOption.OverrideSeconds = EditorGUILayout.Toggle(
            new GUIContent("Override Seconds","Override the number of seconds this object takes to fade to it's final alpha"), 
            fadeOption.OverrideSeconds);
        if (fadeOption.OverrideSeconds)
        {
            fadeOption.Seconds = EditorGUILayout.FloatField(fadeOption.Seconds);
            if (fadeOption.Seconds < 0)
                fadeOption.Seconds = 0;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        fadeOption.OverrideShader = EditorGUILayout.Toggle(
            new GUIContent("Override Shader","Override the shader that is used for this object's fading"), 
            fadeOption.OverrideShader);
        if (fadeOption.OverrideShader)
            fadeOption.FadeShader = (Shader)EditorGUILayout.ObjectField(fadeOption.FadeShader, typeof(Shader), true);
        EditorGUILayout.EndHorizontal();

        if (!fadeOption.OverrideFinalAlpha && !fadeOption.OverrideSeconds && !fadeOption.OverrideShader)
            EditorGUILayout.HelpBox("You don't need this script if you are not overriding anything", MessageType.Warning);
    }
}