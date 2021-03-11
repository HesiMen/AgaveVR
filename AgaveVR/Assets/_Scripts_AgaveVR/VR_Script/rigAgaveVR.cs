using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class rigAgaveVR : UnityEngine.XR.Interaction.Toolkit.XRRig
{

    public float smoothRotationSpeed= 1f;
    public void RotateCamSmooth(float angleDegrees)
    {
        Vector3 rotation = new Vector3(0f, angleDegrees, 0f);
        rig.transform.DORotate(rotation, smoothRotationSpeed);
    }

}
