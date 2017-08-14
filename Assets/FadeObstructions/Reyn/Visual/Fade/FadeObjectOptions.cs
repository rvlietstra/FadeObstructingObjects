using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Place this script on an object that needs to override the fade options
/// Override the fade objects per object
/// </summary>
public class FadeObjectOptions : MonoBehaviour
{
    public bool OverrideSeconds = false;
    public float Seconds = 0;


    public bool OverrideFinalAlpha = false;
    public float FinalAlpha = 0;

    public bool OverrideShader = false;
    public Shader FadeShader = null;
}