using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RotateObject : MonoBehaviour
{
    private Quaternion _originalRot;
    private Vector3 _lastPos, _currentPos;
    private float _rotationSpeed = -0.2f;
    [SerializeField] Camera _GUIcamera;
    private float _FOV;
    private float _originalFOV;
    void Start()
    {
        _lastPos = Input.mousePosition;
        _originalRot = transform.rotation;
        _originalFOV = _GUIcamera.fieldOfView;
    }

    void Update()
    {
        ProcessRotation();
        ProcessZoominIn();
    }

    private void ProcessZoominIn()
    {
        if(Input.GetMouseButton(1))
        {
            _FOV = 2f;
            _GUIcamera.fieldOfView -= _FOV;
            _GUIcamera.fieldOfView = Math.Clamp(_FOV, 50f, 100f);
        }
        else
        {
            _GUIcamera.fieldOfView = _originalFOV;
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {

            transform.RotateAround(transform.position, Vector3.up, 9f * _rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {

            transform.RotateAround(transform.position, Vector3.up, -9f * _rotationSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {

            transform.RotateAround(transform.position, Vector3.right, 9f * _rotationSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {

            transform.RotateAround(transform.position, Vector3.right, -9f * _rotationSpeed);
        }
    }

    private void OnDisable()
    {
        transform.rotation= _originalRot;
    }
}
