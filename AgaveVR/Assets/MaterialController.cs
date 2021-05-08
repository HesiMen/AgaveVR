using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Color32 grassColor;
    public float displacementScale;
    //public float windLeanScale;
    public float windFrequency;
    public Vector2 XZWindDirection;
    public float windStregnth;
    public float bounceFrequency;
    public float individualVFreq;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    // Start is called before the first frame update
    void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = this.GetComponent<Renderer>();
        UpdateMaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateMaterialPropertyBlock();
    }

    void UpdateMaterialPropertyBlock()
    {
        _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_Color", grassColor);
            _propBlock.SetFloat("_DisplacementScale", displacementScale);
            //_propBlock.SetFloat("_WindLean", windLeanScale);
            _propBlock.SetFloat("_WindFrequency", windFrequency);
            _propBlock.SetFloat("_XWindDirection", XZWindDirection.x);
            _propBlock.SetFloat("_ZWindDirection", XZWindDirection.y);
            _propBlock.SetFloat("_WindStrength", windStregnth);
            _propBlock.SetFloat("_BounceFrequency", bounceFrequency);
            _propBlock.SetFloat("_IndividualVFrequency", individualVFreq);
        _renderer.SetPropertyBlock(_propBlock);
    }

}
