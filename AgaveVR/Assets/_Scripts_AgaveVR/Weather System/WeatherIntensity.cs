using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherIntensity : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float _normalizedInternalIntensity;
    [SerializeField] private List<ParticleSystem> _weatherSystem;
    [SerializeField] public WeatherIntensityComm communicator;
    private float _rawValue;
    private float _maxRawVal;

    private void Start() 
    {
        // Normalizes Intensity values between 0 and 1 and assigns to _normalizedInternalIntensity
        // This value will communicate the current intensity of the weather system from here we can use this value and multiply by _maxRawVal to declare a new _rawValue which will be used for intensity
        /*if (_weatherSystem.GetComponent<ParticleSystem>() != null)
        {
            _rawValue = _weatherSystem.GetComponent<ParticleSystem>().emission.rateOverTimeMultiplier;  
        }

        _normalizedInternalIntensity = (_rawValue / _maxRawVal);*/

        //InstantiateParticleSystems();
    }

    private void Update() 
    {
        _normalizedInternalIntensity = communicator.intensityComm.normalizedIntensity;
        EmissionChange();
        SimSpeed();
    }

    public void EmissionChange()
    {
        if (communicator.emissionOverride)
        {
            foreach(ParticleSystem system in _weatherSystem)
            {
                var weatherEmission = system.emission;
                
                weatherEmission.rateOverTimeMultiplier = _normalizedInternalIntensity * communicator.maxParticles[0];

            }
        }
    }

    public void SimSpeed()
    {
        if (communicator.simSpeedOverride)
        {
            int index = 0;
            foreach(ParticleSystem system in _weatherSystem)
            {
                var mainModule = system.main;
                mainModule.simulationSpeed = communicator.simSpeed[index] * _normalizedInternalIntensity;
                index++;
            }
        }
        //var mainModule = _weatherSystem.GetComponent<ParticleSystem>().main;
        //mainModule.simulationSpeed = communicator.simSpeed[0];
        
    }

    public void InstantiateParticleSystems()
    {
        foreach(ParticleSystem system in communicator.weatherSystem)
        {
            ParticleSystem weatherSys = Instantiate(system, this.transform.position + communicator.spawnOffset, Quaternion.identity);

            weatherSys.transform.SetParent(this.gameObject.transform);

            _weatherSystem.Add(weatherSys);
        }
    }
}
