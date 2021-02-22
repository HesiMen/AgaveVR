using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureBase : MonoBehaviour
{

    // For gestures there are going to be different behaviours.
    //For Receiving is going to be a simple Gesture - Place Hands on top of other hands outline
    //FireMaking - Place Hand and move back and forward - Once inside 
    //

    [FMODUnity.EventRef]
    [SerializeField] private string soundClip;

    FMOD.Studio.EventInstance soundState;


    public enum Gesture { Receiving, FireMaking, MakingHole, None }

    public Gesture gesture = Gesture.None;

    public GameObject meshToShow;

    protected virtual void Start()
    {
        soundState = FMODUnity.RuntimeManager.CreateInstance(soundClip);    //call sound  via event ref
    }

    public virtual void ActivateGesture()
    {
      
    }

    public virtual void PlayGestureSoudn()
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundClip);
    }

    public virtual void ShowMesh()
    {
        meshToShow.SetActive(true);
    }

    public virtual void StopShowing()
    {
        meshToShow.SetActive(false);
    }



}
