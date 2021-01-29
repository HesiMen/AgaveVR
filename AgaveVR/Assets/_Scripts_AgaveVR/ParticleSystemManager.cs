using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private NormalizedIntensitySO _generalControl;
    [SerializeField] private NormalizedIntensitySO _normalizedIntensity;
    [SerializeField] private ParticleSystem system;
    [SerializeField] private bool _debugIntensity;
    [SerializeField, Range(0,1)] private float _intensity;
    [SerializeField] private float _maxParticles;

    [Header("Sim Speed")]
    [SerializeField] private bool simSpeedOverride;
    [SerializeField] private float simSpeedMin;
    [SerializeField] private float simSpeedMax;
    private float t;
    private float lerpSimSpeed;

    [Header("Material")]
    [SerializeField] private MeshRenderer _secondObject;
    [SerializeField] private List<string> _propertyNames;
    [SerializeField] private List<float> _propertyValues;
    // Start is called before the first frame update
    void Start()
    {
        //_normalizedIntensity = emissionRateModule.intensityComm;
    }

    // Update is called once per frame
    void Update()
    {
        GeneralIntensityControl();
        //DebugIntensity();
        EmissionComm();
        MaterialComm();
        SimSpeedComm();
    }
    
    public void GeneralIntensityControl()
    {   
        if (_generalControl != null)
            _normalizedIntensity.normalizedIntensity = _generalControl.normalizedIntensity;
        else
            DebugIntensity();
    }

    public void EmissionComm()
    {
        
        var particleEmission = system.emission;
        particleEmission.rateOverTimeMultiplier = _maxParticles * _normalizedIntensity.normalizedIntensity;
    }

    public void SimSpeedComm()
    {
        if (simSpeedOverride)
        {
            var mainModule = system.main;
            t = _normalizedIntensity.normalizedIntensity;
            lerpSimSpeed = Mathf.Lerp(simSpeedMin, simSpeedMax, t);
            mainModule.simulationSpeed = lerpSimSpeed;
        }
    }

    public void MaterialComm()
    {
        if (_secondObject != null)
        {
            for(int i = 0; i < _propertyNames.Count; i++)
            _secondObject.material.SetFloat(_propertyNames[i], _propertyValues[i] * _normalizedIntensity.normalizedIntensity);
        }
    }

    public void DebugIntensity()
    {
        if (_debugIntensity)
            _normalizedIntensity.normalizedIntensity = _intensity;
    }
}
