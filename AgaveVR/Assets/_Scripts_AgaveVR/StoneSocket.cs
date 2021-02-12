using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSocket : UnityEngine.XR.Interaction.Toolkit.XRSocketInteractor
{

    protected override void OnTriggerEnter(Collider col)
    {
        if (col.GetComponentInParent<AgaveObject>() != null && col.GetComponentInParent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.SmallRock)
        {

            base.OnTriggerEnter(col);
        }
    }

}
