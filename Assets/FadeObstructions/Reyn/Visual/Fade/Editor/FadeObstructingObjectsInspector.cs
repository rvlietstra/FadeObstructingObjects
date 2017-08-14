using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(FadeObstructingObjects))]
public class FadeObstructingObjectsInspector : Editor
{
    public override void OnInspectorGUI()
    {
        FadeObstructingObjects fade = target as FadeObstructingObjects;

        fade.FadeShader = (Shader)EditorGUILayout.ObjectField(new GUIContent("Fade Shader", "We replace your shaders with this one, there is one included in the package, drag it in here. If you would like to override this on a specific object you can place a FadeObjectOptions script on that object."), fade.FadeShader, typeof(Shader), true);

        fade.Camera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "Fading is camera dependant, we fade the objects between something and the camera"), fade.Camera, typeof(Camera), true);

        fade.FinalAlpha = EditorGUILayout.Slider(new GUIContent("Final Alpha", "The final alpha of the objects that get faded. If you would like to override this on a specific object you can place a FadeObjectOptions script on that object."), fade.FinalAlpha, 0.0f, 1.0f);
        if (fade.FinalAlpha < 0)
            fade.FinalAlpha = 0;
        if (fade.FinalAlpha > 1)
            fade.FinalAlpha = 1;            

        fade.Seconds = EditorGUILayout.FloatField(new GUIContent("Seconds", "Specify the number of seconds it takes for objects to fade. If you would like to override this on a specific object you can place a FadeObjectOptions script on that object."), fade.Seconds);
        if (fade.Seconds < 0)
            fade.Seconds = 0;

        fade.RayRadius = EditorGUILayout.FloatField(new GUIContent("Radius", "A ray is cast from the camera to your objects that should always be visible, this specifies the ray radius"), fade.RayRadius);
        if (fade.RayRadius < 0)
            fade.RayRadius = 0;

        fade.LayerMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(EditorGUILayout.MaskField("Layer Mask", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(fade.LayerMask), InternalEditorUtility.layers));
    }
}