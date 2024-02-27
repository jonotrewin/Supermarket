using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class Inspect : MonoBehaviour
{
    [SerializeField] public GameObject[] _inspectionObjects;
    [SerializeField] private ProductAudioPlayer _productAudioScript;
    private int _currentIndex;
    public bool _isInspecting;

    public bool _inspectorAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        _inspectorAvailable = true;
    }

    public void EnableInspection(int index)
    {
        
            _currentIndex = index;
            _inspectionObjects[index].SetActive(true);
            _isInspecting = true;
            Cursor.lockState = CursorLockMode.Confined;
            _productAudioScript.PlayProductAudio(_currentIndex);
        
    }

    public void DisableInspection()
    {
        _inspectionObjects[_currentIndex].SetActive(false);
        _isInspecting= false;
        Cursor.lockState = CursorLockMode.Locked;
        _productAudioScript.PlayProductAudio(_currentIndex);
        //_productAudioScript.gameObject.GetComponentInChildren<AudioSource>().Play();
        _productAudioScript.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        //plays the scan beep
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void MakeInspectionAvailable()
    {
        _inspectorAvailable = true;
    }
    public void MakeInspectionUnavailable()
    {
        _inspectorAvailable = false;
    }


}
