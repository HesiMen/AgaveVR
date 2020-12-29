using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsHolePhysics : BeatEvent
{

    public float secondsToWaitEvent = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AgaveObject>() != null && other.gameObject.GetComponent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.Seed)
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.layer = 14;

            StartCoroutine(SeedWasPlanted(secondsToWaitEvent));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AgaveObject>() != null &&  other.gameObject.GetComponent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.Seed)
        {
            other.gameObject.layer = 13;
        }
    }



    
    IEnumerator SeedWasPlanted(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        TaskDone();
    }
}
