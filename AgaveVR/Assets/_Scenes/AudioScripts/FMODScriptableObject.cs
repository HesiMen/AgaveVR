using UnityEngine;

[CreateAssetMenu(fileName = "FMOD-Player-Audio", menuName = "Custom/Audio/Player")]
public class FMODScriptableObject : ScriptableObject 
{
    [Header("Player: Footsteps")]
    [Tooltip("Regular foosteps are recommended to be set to 3D and currently do not have any parameter that influence properties.")]
    [FMODUnity.EventRef] public string regularFoosteps = null;

    [Tooltip("Water foosteps are recommended to be set to 3D and currently do not have any parameter that influence properties.")]
    //[FMODUnity.EventRef] public string waterFootsteps = null;

    [FMODUnity.EventRef] public string crunchSoundPlant = null;
    [FMODUnity.EventRef] public string crunchSoundAnt = null;
    [FMODUnity.EventRef] public string crunchSoundGrasshopper = null;
    [FMODUnity.EventRef] public string dirtScoop = null;

    [FMODUnity.EventRef] public string hunger = null;


    //======================================================================
    [Header("Environment Events and Interactions")]

    [Tooltip("Stone sounds are recommended to be set to 3D and currently has one parameter: EndStoneLoop, which will decide whether the looping stone sound will continue looping or transition into the ending sound. A Value of 0 will continue looping the sound and a value of 1 will ensure that it ends once it reaches the specified marker in FMOD, there is no middle value for this parameter.")]
    [FMODUnity.EventRef] public string stone = null;

    [Tooltip("Seeds are recommended to be set to 3D and currently do not have any parameter that influence properties.")]
    [FMODUnity.EventRef] public string seedGrab = null;

    [Tooltip("Seeds are recommended to be set to 3D and currently do not have any parameter that influence properties.")]
    [FMODUnity.EventRef] public string seedSpawn = null;

    [Tooltip("Seeds are recommended to be set to 3D and currently do not have any parameter that influence properties.")]
    [FMODUnity.EventRef] public string seedDespawn = null;


    //======================================================================
    [Header("Other")]

    [Tooltip("Flat Sounds are recommended to be set to 2Ds and currently do not have any parameter that influence properties.")]
    [FMODUnity.EventRef]
    public string flatSound = null;

}