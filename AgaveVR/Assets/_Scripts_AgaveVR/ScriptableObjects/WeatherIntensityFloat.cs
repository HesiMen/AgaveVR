using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherIntensityFloat", menuName = "Custom/Variables/WeatherIntensityFloat", order = 0)]
public class WeatherIntensityFloat : ScriptableObject 
{
    [Range(0,1)]
    public float normalizedIntensity;
}
