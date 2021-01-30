using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;

//[Serializable]
//public class  MyEvent : UnityEvent { }

public class TriggerEnterTesting : MonoBehaviour
{
    //public MyEvent triggerPlayer;

    public FollowHand followHand;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            //triggerPlayer.Invoke();
            followHand.FollowNow(other.transform);
        }

    }


    private void OnTriggerExit(Collider other)
    {

        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            //triggerPlayer.Invoke();
            followHand.StopFollowing();
        }
    }
}

