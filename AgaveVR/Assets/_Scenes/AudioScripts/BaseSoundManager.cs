using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundManager : MonoBehaviour
{
    public virtual void PlaySoundSimple(string stringRef, Vector3 pos)
    {

        FMODUnity.RuntimeManager.PlayOneShot(stringRef, pos);
        


    }

    public virtual void PlayAndAttach(FMOD.Studio.EventInstance fmodInstance , Transform trans, Rigidbody rb)
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmodInstance, trans, rb);
    }
}
