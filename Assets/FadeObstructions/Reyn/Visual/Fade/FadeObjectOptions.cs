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
    public bool OverrideFadeOutSeconds = false;
    public bool OverrideFadeInSeconds = false;
    public float FadeOutSeconds = 0;
    public float FadeInSeconds = 0;
    public bool OverrideFinalAlpha = false;
    public float FinalAlpha = 0;
}