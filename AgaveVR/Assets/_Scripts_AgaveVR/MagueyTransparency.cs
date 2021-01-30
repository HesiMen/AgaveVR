using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class LerpEvent : UnityEvent { }
public enum BlendMode
{
    Alpha,   // Old school alpha-blending mode, fresnel does not affect amount of transparency
    Premultiply, // Physically plausible transparency mode, implemented as alpha pre-multiply
    Additive,
    Multiply
}

public class MagueyTransparency : MonoBehaviour
{
    public LerpEvent FinisedAlphaLerp;

    [Header("General")]
    public GameObject maguey;
    private Renderer[] magueyRend;
    [SerializeField]
    private bool alphaToggle;

    public BlendMode blendMode;
    [SerializeField] [Range(-1,1)]
    public float objectAlpha;
    [SerializeField]
    private Color mainColor;
    [SerializeField]
    public bool lerpAlpha;

    [Header("Blend Mode: Alpha")]
    [SerializeField] [Range(-1,1)]
    private float minAlpha;
    [SerializeField] [Range(-1,1)]
    private float maxAlpha;
    [SerializeField]
    private float lerpRate;
    [SerializeField] [Range(-1, 1)]
    private float initialLerpState;

    [Header("Blend Mode: Premultiply & Additive")]
    [SerializeField] [Range(-1,1)] private float minPreM;
    [SerializeField] [Range(-1,1)] private float maxPreM;
    [SerializeField] private float lerpRatePreM;
    [SerializeField] private float initialLerpStatePreM;
    private bool reversing;

    [Range(-1,1)]
    private int negative;
    
    

    public void LerpAlphaBOOL()
    {
        lerpAlpha = true;

    }
    void Start()
    {
        magueyRend = maguey.GetComponentsInChildren<Renderer>();
        mainColor = magueyRend[0].material.GetColor("_Color");

        negative = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ObjectTransparency();

        if(lerpAlpha)
            alphaToggle = true;
        //Debug.Log(magueyrend.material.renderQueue);
        //Debug.Log(magueyrend.material.GetColor("_Color"));
    }

    public void ObjectTransparency()
    {
        mainColor.a = objectAlpha;

        if (alphaToggle)
        {

            foreach(Renderer rend in magueyRend)
            {
                //blendMode = (BlendMode) rend.material.SetFloat("_Blend", alphaBlendType);

                // Specific Transparent Mode Settings
                switch (blendMode)
                {
                    case BlendMode.Alpha:
                        rend.material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
                        rend.material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        break;
                    case BlendMode.Premultiply:
                        rend.material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                        rend.material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        rend.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        break;
                    case BlendMode.Additive:
                        rend.material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
                        rend.material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.One);
                        rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        break;
                    case BlendMode.Multiply:
                        rend.material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.DstColor);
                        rend.material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                        rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        rend.material.EnableKeyword("_ALPHAMODULATE_ON");
                        break;
                }
                

                rend.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                rend.material.SetColor("_Color", mainColor);
                rend.material.SetOverrideTag("RenderType", "Transparent");
                rend.material.SetInt("_ZWrite",1);

                rend.material.renderQueue += rend.material.HasProperty("_QueueOffset") ? (int) rend.material.GetFloat("_QueueOffset") : 0;
                rend.material.SetShaderPassEnabled("ShadowCaster", false);

            }
            if (lerpAlpha)
            {
                alphaToggle = true;
                LerpAlpha();
            }
        }

        else
        {
            foreach(Renderer rend in magueyRend)
            {
                rend.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                rend.material.SetOverrideTag("RenderType", "Opaque");
                rend.material.SetColor("_Color", mainColor);
                objectAlpha = 1;

                rend.material.SetInt("_ZWrite",1);
            }
        }
    }


    public void LerpAlpha()
    {
        switch(blendMode)
        {
            case BlendMode.Alpha:
                objectAlpha = Mathf.Lerp(minAlpha, maxAlpha, initialLerpState);

                if (initialLerpState < 0f)
                {
                    objectAlpha = 0;
                    initialLerpState = 0;
                    FinisedAlphaLerp.Invoke();
                }

                if (initialLerpState > 1f)
                {
                    objectAlpha = 1;
                    initialLerpState = 1;
                }

                initialLerpState += (negative * lerpRate) * Time.deltaTime;
            break;

            case BlendMode.Premultiply:
                objectAlpha = Mathf.Lerp(minPreM, maxPreM, initialLerpStatePreM);

                if (initialLerpStatePreM < 0f)
                {
                    objectAlpha = -1;
                    initialLerpStatePreM = 0;
                    reversing = true;
                    negative = -1;
                }

                if (reversing && (initialLerpStatePreM >= .5f))
                {
                    initialLerpStatePreM = .5f;
                    negative = 1;
                    blendMode = BlendMode.Additive;
                    objectAlpha = 1;
                }

                if (initialLerpStatePreM > 1f)
                {
                    objectAlpha = 1;
                    initialLerpStatePreM = 1;
                }

                initialLerpStatePreM += (negative * lerpRatePreM) * Time.deltaTime;
            break;

            case BlendMode.Additive:
                if (reversing)
                {
                    initialLerpStatePreM = 1;
                    reversing = false;
                }

                objectAlpha = Mathf.Lerp(minPreM, maxPreM, initialLerpStatePreM);

                if (initialLerpStatePreM < .5f)
                {
                    objectAlpha = 0;
                    initialLerpStatePreM = .5f;
                }

                if (initialLerpStatePreM >= 1f)
                {
                    objectAlpha = 1;
                    initialLerpStatePreM = 1;
                }

                initialLerpStatePreM += (negative * lerpRatePreM) * Time.deltaTime;

            break;
        }
    }

    public void ResetValues()
    {
        objectAlpha = 1;
        alphaToggle = false;
        blendMode = BlendMode.Alpha;

        //Blend Mode: Alpha
        minAlpha = 0;
        maxAlpha = 1;
        lerpRate = -0.2f;
        initialLerpState = 1;

        //Blend Mode: PreMultiply & Additive
        minPreM = -1;
        maxPreM = 1;
        lerpRatePreM = -0.4f;
        initialLerpStatePreM = 1;
        negative = 1;

    }
}
