using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDigAnimManager : MonoBehaviour
{
    [SerializeField] private MeshCollider _flatCollision;
    [SerializeField] private MeshCollider _holeCompleteCollision;
    [SerializeField] private Transform _holeMask;
    [SerializeField] private SphereCollider _physicsLayerTrigger;
    [SerializeField] [Range(0,1)] private float _blendShapeAmount;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    
    private Vector3 _originalMaskScale;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        _mesh.SetBlendShapeWeight(0, _blendShapeAmount * 100f);
        _originalMaskScale = new Vector3 (_holeMask.localScale.x, _holeMask.localScale.y, _holeMask.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        _mesh.SetBlendShapeWeight(0, _blendShapeAmount * 100f);
        ScaleMask();
        ChangeCollision();

    }

    public void ChangeCollision()
    {
        if (_blendShapeAmount == 1f)
        {
            _flatCollision.enabled = false;
            _holeCompleteCollision.enabled = true;
        }

        if(_blendShapeAmount == 0f)
        {
            _flatCollision.enabled = true;
            _holeCompleteCollision.enabled = false;
        }
    }

    public void ScaleMask()
    {
        _holeMask.localScale = new Vector3(_originalMaskScale.x * _blendShapeAmount, _originalMaskScale.y * _blendShapeAmount, _originalMaskScale.z * _blendShapeAmount);
    }
}
