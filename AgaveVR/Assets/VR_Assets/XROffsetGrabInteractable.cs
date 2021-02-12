using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    void Start()
    {

      
        // Create attach point
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }

        base.OnSelectEntered(interactor);
    }

    //ignore player collision if being grabbed

 
    protected override void OnSelectExiting(XRBaseInteractor interactor)// This fucks up a bit when passing from one hand to another since that reads as exiting
    {
        base.OnSelectExiting(interactor);


        //IgnorePlayerCollision(false);// moving this when object is held

    }

    private List<Collider> playerCollider = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerCollider.Contains(collision.collider)) // if is player and collider is not in the list add and ignore it. 
        {
            playerCollider.Add(collision.collider);
            //IgnorePlayerCollision(true);

        }
    }

    public void IgnorePlayerCollision(bool _active) 
    {
        Debug.Log("Ignoring PlayerCollision: " + _active);
        if (playerCollider != null)
        {
            foreach (var coll in colliders)
            {
                foreach (var playerCol in playerCollider)
                {
                    Physics.IgnoreCollision(coll, playerCol, _active);
                }
            }
        }
        
    }


    //protected override void OnSelectExit(XRBaseInteractor interactor)
    //{
    //    base.OnSelectExit(interactor);



    //    if(this.gameObject.GetComponent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.SmallRock)
    //    {
    //        GetComponent<Rigidbody>().isKinematic = true;
    //    }

    //   }

}
