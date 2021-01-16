using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MateriaRenderQueue : MonoBehaviour
{
    public Renderer mask;
    public Renderer hole;
    public int maskRenderQueue= 2000;
    private void OnValidate()
    {
        //maskRenderQueue = hole.material.renderQueue;
        mask.material.renderQueue = maskRenderQueue;
        //mask.material.SetBuffer();

        Debug.Log(mask.material.renderQueue);
        Debug.Log(hole.material.renderQueue);
    }
}
