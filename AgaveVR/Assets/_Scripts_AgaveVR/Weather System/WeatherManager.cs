﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStartupPresets
{
    Light, Mid, Heavy, Custom
}
public class WeatherManager : MonoBehaviour
{  
    [SerializeField, Range(0,1)] private float intensity;
    [SerializeField] public NormalizedIntensitySO _normalizedInternalIntensity;

    [SerializeField] private WeatherStartupPresets _startUpRatePreset;

    [SerializeField] private float startupRate;
    [SerializeField, Range(0,1)] private float intensityTarget;
    [SerializeField] private bool DebugMode;

    [Header("Weather")]
    [SerializeField] private bool activateWeather;
    [SerializeField] private List<NormalizedIntensitySO> weatherSystemComms;
    [SerializeField] private List<GameObject> _weatherSystem;

    private float minLerp = 0;
    private float maxLerp = 1;
    [SerializeField] private float t = 0.0f;

    // Start is called before the first frame update
    private void Start() 
    {
    }
    // Update is called once per frame
    void Update()
    {
    
        WeatherEasing();

        for (int i = 0; i < weatherSystemComms.Count; i++)
        {
            _normalizedInternalIntensity.normalizedIntensity = intensity;

            weatherSystemComms[i].normalizedIntensity = _normalizedInternalIntensity.normalizedIntensity;

            if (DebugMode)
                intensity = intensityTarget;
        }
    }



    public void WeatherEasing()
    {
        switch(_startUpRatePreset)
        {
            case WeatherStartupPresets.Light:
                startupRate = .01f;
                break;
            case WeatherStartupPresets.Mid:
                startupRate = 0.25f;
                break;
            case WeatherStartupPresets.Heavy:
                startupRate = 0.5f;
                 break;
            case WeatherStartupPresets.Custom:
                break;
            default:
                break;
        }
 
        switch(activateWeather && intensityTarget > t)
        {
            case true:
                intensity = Mathf.Lerp(minLerp, maxLerp, t);
                if (t != intensityTarget)
                    t += startupRate * Time.deltaTime;
                if (t > intensityTarget)
                    t = intensityTarget;
                break;
            
            case false:
                intensity = Mathf.Lerp(minLerp, maxLerp, t);
                if (t != intensityTarget)
                    t -= startupRate * Time.deltaTime;
                if (t < intensityTarget)
                    t = intensityTarget;
                break;
        }
    } 
}