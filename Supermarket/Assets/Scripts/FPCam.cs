using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class FPCam : MonoBehaviour
{
    [SerializeField,Range(0.1f,15f)] float _sensitivity;
    private Vector2 _mouseInput;
    private Vector2 _rotation = Vector3.zero;

    [SerializeField] float yLimit;
    [SerializeField] float xLimit;

    [SerializeField] Inspect _inspection;
    private Vector3 _currentPos;
    private Vector3 _lastPos;
    CameraSwitch _cameraSwitch;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _lastPos = _currentPos;
        _cameraSwitch = GetComponent<CameraSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        _lastPos = Input.mousePosition;

        if(!_inspection._isInspecting && !_cameraSwitch.IsSwitching && _cameraSwitch.ISwitchedToMain)
        {
            _mouseInput.x += Input.GetAxis("Mouse X");
            _mouseInput.y += -Input.GetAxis("Mouse Y");

            _rotation.x = _mouseInput.x * _sensitivity;
            _rotation.y = _mouseInput.y * _sensitivity;

            _rotation.x = Mathf.Clamp(_rotation.x, -xLimit, xLimit);
            _rotation.y = Mathf.Clamp(_rotation.y, -yLimit, yLimit);

            //transform.localEulerAngles = Vector3.right * _rotation.y;
            //transform.Rotate(Vector3.up * _rotation.x);

            transform.localRotation = Quaternion.Euler(_rotation.y, _rotation.x, 0f);
        }
        
       

        


    }
}
