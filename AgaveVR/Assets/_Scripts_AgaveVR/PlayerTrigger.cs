using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerOnTriggerEnvet : UnityEvent { }
public class PlayerTrigger : MonoBehaviour
{
    public PlayerOnTriggerEnvet onPlayerTrigger;
    public PlayerOnTriggerEnvet onPlayerOffTrigger;

    public bool notShow = false;


    private void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponentInParent<AgaveObject>() !=null && other.GetComponentInParent<AgaveObject>()._isHeld)
        //{
        //    _HasSeed = true;
        //}

        if (other.CompareTag("Player") && !notShow)
        {
            onPlayerTrigger.Invoke();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerOffTrigger.Invoke();
        }
    }

    public void WasSeeded(bool hasSeed)
    {
        notShow = hasSeed;

        
    }
}
