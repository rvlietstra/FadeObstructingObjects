using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(FadeObstructionsManager))]
public class FadeObstructingObjectsInspector : Editor
{
    public override void OnInspectorGUI()
    {
        FadeObstructionsManager fade = target as FadeObstructionsManager;

        fade.Camera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "Fading is camera dependant, we fade the objects between something and the camera"), fade.Camera, typeof(Camera), true);

        fade.FinalAlpha = EditorGUILayout.Slider(new GUIContent("Final Alpha", "The final alpha of the objects that get faded. If you would like to override this on a specific object you can place a FadeObjectOptions script on that object."), fade.FinalAlpha, 0.0f, 1.0f);
        if (fade.FinalAlpha < 0)
            fade.FinalAlpha = 0;
        if (fade.FinalAlpha > 1)
            fade.FinalAlpha = 1;            

        fade.FadeOutSeconds = EditorGUILayout.FloatField(new GUIContent("Fade Out Seconds", "Specify the number of seconds it takes for objects to fade out. If you would like to override this on a specific object you can place a FadeObjectOptions script on that object."), fade.FadeOutSeconds);
        if (fade.FadeOutSeconds < 0)
            fade.FadeOutSeconds = 0;

        fade.FadeInSeconds = EditorGUILayout.FloatField(new GUIContent("Fade In Seconds", "Specify the number of seconds it takes for objects to fade back in. If you would like to override this on a specific object you can place a FadeObjectOptions script on that object."), fade.FadeInSeconds);
        if (fade.FadeInSeconds < 0)
            fade.FadeInSeconds = 0;


        fade.RayRadius = EditorGUILayout.FloatField(new GUIContent("Radius", "A ray is cast from the camera to your objects that should always be visible, this specifies the ray radius"), fade.RayRadius);
        if (fade.RayRadius < 0)
            fade.RayRadius = 0;

        fade.LayerMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(EditorGUILayout.MaskField("Layer Mask", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(fade.LayerMask), InternalEditorUtility.layers));
    }
}