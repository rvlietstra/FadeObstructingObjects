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
        fadeOption.OverrideFadeOutSeconds = EditorGUILayout.Toggle(
            new GUIContent("Fade Out Seconds","Override the number of seconds this object takes to fade to it's final alpha"), 
            fadeOption.OverrideFadeOutSeconds);
        if (fadeOption.OverrideFadeOutSeconds)
        {
            fadeOption.FadeOutSeconds = EditorGUILayout.FloatField(fadeOption.FadeOutSeconds);
            if (fadeOption.FadeOutSeconds < 0)
                fadeOption.FadeOutSeconds = 0;
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        fadeOption.OverrideFadeInSeconds = EditorGUILayout.Toggle(
            new GUIContent("Fade In Seconds", "Override the number of seconds this object takes to fade to it's final alpha"),
            fadeOption.OverrideFadeInSeconds);
        if (fadeOption.OverrideFadeInSeconds)
        {
            fadeOption.FadeInSeconds = EditorGUILayout.FloatField(fadeOption.FadeInSeconds);
            if (fadeOption.FadeInSeconds < 0)
                fadeOption.FadeInSeconds = 0;
        }
        EditorGUILayout.EndHorizontal();

    }
}