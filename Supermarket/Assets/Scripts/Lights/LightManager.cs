using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] Light[] _mainLights;
    [SerializeField] Light[] _spotLights;
    [SerializeField] Light _dirLight;
    float _currentDirLightIntensity;
    [SerializeField,Range(0.0f, 1.0f)] float _dimIntensity;
    FlickerControl _lightFlick;
    [SerializeField] int delay;

    bool _areLightsTurningOff = false;
    public bool _isSwitchingLighting =false ;
    public bool _areMainLightsOff = false;
    void Start()
    {
        _currentDirLightIntensity = _dirLight.intensity;
        DisableSpotLights();
    }

    private void DisableSpotLights()
    {
        foreach (Light light in _spotLights)
        {
            light.enabled = false;
        }
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    _isSwitchingLighting = true;
        //}
        if(_isSwitchingLighting)
        {
            StartCoroutine(TurnOffMainLights());
            DimLights();
        }
        TurnSpotlightsOn();
    }

    private bool AreMainLightsOff()
    { 
        foreach(Light light in _mainLights)
        {
            if (light.enabled)
            {
                return false;
            }
        }
        return true;
    }

    private void DimLights()
    {
        _dirLight.intensity = Mathf.Lerp(_currentDirLightIntensity, _dimIntensity, 0.5f);
    }

    IEnumerator TurnOffMainLights()
    {
        _areLightsTurningOff = true;
        foreach (Light mainLight in _mainLights)
        {
            int rIndex = UnityEngine.Random.Range(0, _mainLights.Length);
            _mainLights[rIndex].enabled = false;
            yield return new WaitForSeconds(delay);
        }

        _areLightsTurningOff = false;
    }
    private void TurnSpotlightsOn()
    {
        if(AreMainLightsOff())
        {
            Invoke("EnableSpotlights", 5f);
        }

    }

    private void EnableSpotlights()
    {
        foreach (Light light in _spotLights)
        {
            light.enabled = true;
            _dirLight.enabled = false;
        }
    }
}
