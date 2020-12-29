using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class Teleport_WorldArea : TeleportationArea
    {
        // Override the teleport request to use the world rotation and not our gameObject's transform
        protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            teleportRequest.destinationPosition = raycastHit.point;
           // teleportRequest.destinationUpVector = Vector3.up; // use the area transform for data.
           // teleportRequest.destinationForwardVector = Vector3.forward;
            teleportRequest.destinationRotation = Quaternion.identity;
            return true;
        }
    }
}