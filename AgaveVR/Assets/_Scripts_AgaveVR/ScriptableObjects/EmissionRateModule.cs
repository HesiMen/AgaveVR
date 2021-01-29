using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmissionRateModule", menuName = "Custom/Weather/Modules/EmissionRateModule")]
public class EmissionRateModule : ScriptableObject 
{
    public NormalizedIntensitySO intensityComm;
    public List<ParticleSystem> system;

    [Header("Emission Rate")]
    public bool emissionOverride;

    [Tooltip("Numbered by the particle system being referenced")]
    public List<float> maxParticles;  

    [Header("Other Objects to Spawn")]
    public bool materialPropertyOverride;
    public GameObject sideObject;
    public List<string> materialPropertyNames;
    public List<float> maxMaterialPropertyValues;  
}
