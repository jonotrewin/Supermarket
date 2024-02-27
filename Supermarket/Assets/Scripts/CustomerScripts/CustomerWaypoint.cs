using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaypoint : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool _isOccupied;

    public bool _isExitWaypoint = false;

    public GameObject _overlappingCustomer;
    public Customer _overlappingCustomerData;

    public CustomerManager _manager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        _isOccupied = true;
        _overlappingCustomer = other.gameObject;
        other.gameObject.TryGetComponent<Customer>(out _overlappingCustomerData);
        

        

        
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isExitWaypoint)
        {
            _isOccupied = false;
            _overlappingCustomer = null;
            _overlappingCustomerData = null;
        }

    }

    
}
