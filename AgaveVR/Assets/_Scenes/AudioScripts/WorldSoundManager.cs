using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSoundManager : BaseSoundManager
{
    public static WorldSoundManager i;
    [Header("Stone Sounds")]
    [FMODUnity.EventRef]
    public string beatCompletePickUp, beatCompletePlaced, stoneRise, stoneLaunch, stoneKill;

    [Header(" Ambience Sounds")]
    [FMODUnity.EventRef]
    public string ambDay, ambNight, rain1, rain2, rain3, wind1, wind2, wind3;


    //rockInstance

    public FMOD.Studio.EventInstance stoneRiseInstance;
    public FMOD.Studio.EventInstance stoneLaunchInstance;
    public FMOD.Studio.EventInstance stoneKillInstance;
    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        stoneRiseInstance = FMODUnity.RuntimeManager.CreateInstance(stoneRise);
        stoneLaunchInstance = FMODUnity.RuntimeManager.CreateInstance(stoneLaunch);
        stoneKillInstance = FMODUnity.RuntimeManager.CreateInstance(stoneKill);
    }
}
