using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] public Inspect _inspectedItem;
    [SerializeField] public GameObject _inspection;
    [SerializeField] public int _index;

    [SerializeField] bool _isSelecting;
    [SerializeField] bool _isInspecting;
    private Vector3 _mousePos;
    [SerializeField] public GameObject _currentlySelectedProduct;
    // Start is called before the first frame update
    void Start()
    {
        //_inspection = GameObject.FindGameObjectWithTag("InspectionManager");
        //_inspectedItem = FindAnyObjectByType<Inspect>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inspectedItem._inspectorAvailable)
        {
            ProcessSelection();
        }
    }

    private void ProcessSelection()
    {
        _mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        RaycastHit hitInfo;
        
        Color color = GetComponent<MeshRenderer>().material.color;
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, 5000f))
        {
            color.a = 0.6f;
           
            if (Input.GetMouseButtonDown(0) && !_inspectedItem._isInspecting)
            {
               
                _inspection.SetActive(true);
                _inspectedItem.EnableInspection(_index);
                //Debug.Log(hitInfo.collider.gameObject.name);
                _currentlySelectedProduct = hitInfo.collider.gameObject;
                
            }
        }
        else
        {
            color.a = 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inspectedItem.DisableInspection();
            _inspection.SetActive(false);
            //Debug.Log("space");
        }

        GetComponent<MeshRenderer>().material.color = color;
    }
}
