using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameteTestAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string mySound;
    public FMOD.Studio.EventInstance mySoundInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        mySoundInstance = FMODUnity.RuntimeManager.CreateInstance(mySound);

        
    }



   public void ChangemySoundNowOfCave(float vol)
    {
        //mySoundInstance.setParameterByName(rideLily, vol);
    }
}
