using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

[Serializable]
public class StemEvent : UnityEvent { }
public class ScaleStem : MonoBehaviour
{
    public Vector3 goal;
    public float scaletime = 3f;


    public StemEvent EndGrow;

    public bool grow;
    private void Start()
    {
        if (grow)
            ScaleTween(goal, scaletime);
    }

    public void StartScale()
    {
        ScaleTween(goal / 3f, scaletime);
    }

    public void MidScale()
    {
        ScaleTween(goal / 1.5f, scaletime);

    }

    public void EndScale()
    {


        transform.DOScale(goal, scaletime).OnComplete(ScaleComplete);
    }



    private void ScaleTween(Vector3 goalV3, float time)
    {
        transform.DOScale(goalV3, time);
    }

    private void ScaleComplete()
    {
        EndGrow.Invoke();
    }
}
