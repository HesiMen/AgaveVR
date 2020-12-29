using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandsNoHold : MonoBehaviour
{
    Transform ogParent;

    private void Start()
    {
       ogParent =  this.transform.parent;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent = ogParent;
        }
    }
}
