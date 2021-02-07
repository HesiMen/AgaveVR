using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

[Serializable]
public class DissolveEnvent: UnityEvent { }

public class AnimatingDissolveSurface : MonoBehaviour
{

    [Range(0, 1)]
    public float ammount; 
    private Renderer[] dissolve;
    public List<Material> materials = new List<Material>();

    public DissolveEnvent DissolveFinished;
    private void Start()
    {
        dissolve = GetComponentsInChildren<Renderer>();

        foreach (var rend in dissolve)
        {
            foreach (var mat in rend.materials)
            {
                materials.Add(mat);
            }
        }
    }



    //public void Update()
    //{
    //    foreach (var mat in materials)
    //    {
    //        mat.SetFloat("_Amount", ammount);
    //    }
    //}


    public void AnimateDissolve(float duration)
    {
        DOTween.To(StartDissolve, 0, 1, duration).OnComplete(DissolveEnded);
    }

    private void StartDissolve(float ammount)
    {
        foreach (var mat in materials)
        {
            mat.SetFloat("_Amount", ammount);
        }
    }
    private void DissolveEnded()
    {
        DissolveFinished.Invoke();
    }

}
