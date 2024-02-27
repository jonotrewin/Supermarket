using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlickerControl : MonoBehaviour
{
    [SerializeField] float _timeDelay;
    [SerializeField] public bool _isFlickering = false;
    private bool _isTimeToFlicker = false;
    CameraSwitch _cameraSwitch;
    [SerializeField] Light[] flickeringLights;
    [SerializeField] Light _dirLight;
    public bool IsTimeToFlicker
    {
        get { return _isTimeToFlicker;}
        set { _isTimeToFlicker = value;}
    }
    private void Awake()
    {
        _cameraSwitch = GameObject.FindAnyObjectByType<CameraSwitch>();
    }
    void Update()
    {
        //test logic NOT TO BE IMPLEMENTED HERE.
        //should be called inside logic that triggers this effect and sets the property to true
        if(_cameraSwitch.IsSwitching)
        {
            _isTimeToFlicker = true;
        }else _isTimeToFlicker= false;

        FlickerEffect(_isTimeToFlicker);
    }

    private void FlickerEffect(bool isTimeToFlicker)
    {
        while(isTimeToFlicker )
        {
            if(!_isFlickering)
            {
                StartCoroutine(Flickering());
            }
            isTimeToFlicker = false;
        }
    }

    IEnumerator Flickering()
    {
        foreach(Light fl in flickeringLights)
        {
            _isFlickering = true;
            fl.enabled = false;
            _timeDelay = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(_timeDelay);
            fl.enabled = true;
            _timeDelay = Random.Range(0.05f, 0.2f);
            _isFlickering = false;
        }

    }
}
