using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardThrow : BeatEvent
{

    Rigidbody rb;
    public float thrust = 500f;
    public Vector3 forceDirection;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();


        rb.AddForce(forceDirection * thrust);

        TaskDone();

    }
}
