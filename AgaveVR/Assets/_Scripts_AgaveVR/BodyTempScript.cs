using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BodyTempScript: MonoBehaviour
{
    // Check interaction affecting body temperature, notify other systems of change
    public delegate void TempChange(float degrees); //how much temp to add/subtract
    public event TempChange tempEvent;

    public float degreeChange = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // logic for body temp interactions
        if (other.GetComponent<AgaveObject>() == null)
        {
                if(tempEvent != null)
                {
                    // pos change
                    tempEvent(degreeChange);
                    // neg change
                    tempEvent(-degreeChange);
            }
        }
    }
}
