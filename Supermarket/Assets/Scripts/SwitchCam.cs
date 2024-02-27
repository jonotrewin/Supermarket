using System.Collections;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    [SerializeField] Transform[] _waypoints;
    private int _currentIndex = 0;
    private int _nextIndex = 1;
    private float _lerpTime = 2f;
    private bool _isSwitching = false;
    public bool IsSwitching
    {
        get { return _isSwitching; }
    }

    void Start()
    {
        _currentIndex = 0;
    }

    void Update()
    {
        PointToWaypoint();
    }

    private void PointToWaypoint()
    {
        if (!_isSwitching && Input.GetKeyDown(KeyCode.Space))
        {
            _nextIndex = (_currentIndex + 1) % _waypoints.Length;
            StartCoroutine(MoveToWaypoint(_waypoints[_nextIndex]));
            //Debug.Log("space");
        }
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

        // Ensure the object reaches the exact position and rotation of the target waypoint
        transform.position = targetWaypoint.position;
        transform.rotation = targetWaypoint.rotation;

        _currentIndex = _nextIndex;
        _isSwitching = false;
    }
}