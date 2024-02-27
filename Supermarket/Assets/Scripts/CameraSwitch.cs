using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    DialoguePlayer _dialoguePlayer;
    [SerializeField] DialogueManager _dialogueManager;
    [SerializeField] Transform[] _waypoints;
    private int _currentIndex = 0;
    private int _nextIndex = 1;
    private float _lerpTime = 2f;
    private bool _isSwitching = false;
    private bool _isSwitchedToMain = false;
    public bool IsSwitching
    {
        get { return _isSwitching; }
    }
    public bool ISwitchedToMain
    {
        get { return _isSwitchedToMain; }
    }

    public bool _isTimeToSwitch;
    public bool _isTimeToSwitchBack;
    void Start()
    { 
        _currentIndex = 0;
    }

    void Update()
    {
        //ClearInactiveDg();
        Invoke("CheckWaypoint",0.5f);
        PointToWaypoint();
    }

    private void CheckWaypoint()
    {
        if(transform.position == _waypoints[0].position)
        {
            _isSwitchedToMain = true;
        }
        else _isSwitchedToMain = false;
    }

    private void PointToWaypoint()
    {
        if (!_isSwitching && _isTimeToSwitch)
        {
            //_nextIndex = (_currentIndex + 1) % _waypoints.Length;
            StartCoroutine(MoveToWaypoint(_waypoints[1]));
        }
        else if(!_isSwitching && _isTimeToSwitchBack && transform.position == _waypoints[1].position)
            StartCoroutine(MoveToWaypoint(_waypoints[0]));
    }

    IEnumerator MoveToWaypoint(Transform targetWaypoint)
    {
        _isSwitching = true;
        float lerpTime = 0.0f;
        Vector3 initialPos = transform.position;
        Quaternion initialRot = transform.rotation;

        while (lerpTime < 1.0f)
        {
            transform.position = Vector3.Lerp(initialPos, targetWaypoint.position, lerpTime);
            transform.rotation = Quaternion.Lerp(initialRot, targetWaypoint.rotation, lerpTime);

            yield return new WaitForEndOfFrame();
            lerpTime += Time.deltaTime / _lerpTime;
        }

        transform.position = targetWaypoint.position;
        transform.rotation = targetWaypoint.rotation;

        _currentIndex = _nextIndex;
        _isSwitching = false;
    }
}