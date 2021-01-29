using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "NormalizedIntensity", menuName = ("Custom/NormalizedIntensityComm"))]
public class NormalizedIntensitySO : ScriptableObject
{
    [Range(0,1)] public float normalizedIntensity;
}
