using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "WeatherIntensity", menuName = "Custom/WeatherComm")]
public class WeatherIntensityComm : ScriptableObject
{
    [Range(0,1)] public float normalizedIntensity;
    public NormalizedIntensitySO intensityComm;
    public List<ParticleSystem> weatherSystem;

[Header("Emission Rate")]
    public bool emissionOverride;

    [Tooltip("Numbered by the particle system being referenced")]
    public List<float> maxParticles;  

[Header("Simulation Speed")]
    public bool simSpeedOverride;
    public List<float> simSpeed;

[Header("Material Propery")]
    public bool materialPropertyOverride;
    public GameObject sideObject;
    public List<string> materialPropertyNames;
    public List<float> materialPropertyValues;  


[Header("Spawn")]
    public bool spawnOnParentOverride;
    public Vector3 spawnOffset;

}
