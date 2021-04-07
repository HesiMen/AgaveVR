using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EatingScript : MonoBehaviour
{
    // This script is to check what is 
    //being eaten and to send a message to let other systems know when something was eaten
    public delegate void AteSomething(float energy);// how much energy to add?
    public event AteSomething ateEvent;

    public ParticleSystem health;
    //public float energyAdded = 10f; // Should all food same energy? for now yes
    //This can be changed in AgabeObject to each have different values. for now lets keep it simple

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AgaveObject>() != null)
        {
            AgaveObject aObject = other.GetComponentInParent<AgaveObject>();
            //Check for when its grabed
            if (aObject._isEdible)
            {
                //JustMakeitGo away for now.  

                aObject.consumable.Use();
                aObject.gameObject.SetActive(false);
                StartCoroutine(HealthOnce());

                if(aObject.whichSeed == AgaveObject.WhichSeed.NotASeed)
                {
                    PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.eatingBugString, transform.position);
                }
                else
                {
                    PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.eatingPlantString, transform.position);

                }
                //if(ateEvent != null)
                //{
                //    ateEvent(energyAdded);
                //}
            }
        }
    }

    IEnumerator HealthOnce()
    {
        health.Play();
        yield return new WaitForSeconds(1f);
        health.Stop();
    }
}
