using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    [Header("Day Night")]
    [SerializeField] private GameObject _dayNightSky;
    [SerializeField, Range(0,1)] private float _dayNightAlpha;
    [SerializeField, Range(0,1)] private float _dayNightVal;
    [SerializeField] private Vector2 _skyViewLimits;
    [SerializeField] private MeshRenderer _skyMaterial;
    [SerializeField] private float _dayLength;
    [SerializeField] private float _duskDawnTransitionRate;
    [SerializeField] private float _fadeOutLimit;
    private Transform _dayNightTransform;
    private bool _sphereRotate;
    private bool _sunsetTime;
    private float _lerpState;
    private int _swithcLerp;
    [SerializeField] private bool _day;


    [Header("Sunset Sunrise")]
    [SerializeField] private GameObject _sunsetSunriseSky;

    // Start is called before the first frame update
    void Start()
    {
        _swithcLerp = -1;
        _skyMaterial = _dayNightSky.GetComponent<MeshRenderer>();
        _dayNightTransform = _dayNightSky.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _skyMaterial.material.SetFloat("_AlphaValue", _dayNightAlpha);

        DayTransition();
        //NightTransition();
        RotateSky();
    }

    public void AlphaCtrl()
    {
        if (_dayNightTransform.rotation.x >= 60 || _dayNightTransform.rotation.y >= 60 || _dayNightTransform.rotation.z >= 60 || _dayNightTransform.rotation.x <= -60 || _dayNightTransform.rotation.y <= -60 || _dayNightTransform.rotation.z <= -60)
        {
            _sunsetTime = true;
            //_sphereRotate = false;
        }
    }

    public void NightTransition()
    {
        if (!_day)
        {
            _sphereRotate = false;
            _dayNightAlpha = Mathf.Lerp(0, 1, _lerpState);
            _lerpState -= _duskDawnTransitionRate * Time.deltaTime;

            //_skyMaterial.material.SetFloat("_SmoothScalar", _skyViewLimits.x);

            if (_lerpState <= 0)
            {
                _lerpState = 0;
                _dayNightTransform.eulerAngles = new Vector3(180, 0, 0);

                _skyMaterial.material.SetFloat("_SmoothScalar", _skyViewLimits.x);
                _duskDawnTransitionRate *= -1;
            }

            if (_lerpState >= 1f)
                _lerpState = 1;
        }
    }

    public void DayTransition()
    {
        if(_day)
        {
            _dayNightAlpha = Mathf.Lerp(0, 1, _lerpState);
            _lerpState -= _duskDawnTransitionRate * Time.deltaTime;

            if (_lerpState <= 0)
            {
                _lerpState = 0;
                if (_dayNightTransform.eulerAngles.z != 90)
                    _dayNightTransform.eulerAngles = new Vector3(0, Random.Range(0, 360), 90);
                _skyMaterial.material.SetFloat("_SmoothScalar", _skyViewLimits.y);
                _duskDawnTransitionRate *= -1;
            }

            if(_lerpState >= 1)
            {
                _lerpState = 1;
                _sphereRotate = true;
            }
        }
    }

    public void RotateSky()
    {
        if (_dayNightTransform.eulerAngles.z > -90 && _sphereRotate == true)
            _dayNightTransform.Rotate(0, 0, -_dayLength, Space.Self);
    }
}
