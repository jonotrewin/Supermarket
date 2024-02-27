using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{

    //[SerializeField] GameObject _testCustomer;

    [SerializeField] GameObject _divider;

    [SerializeField] List<GameObject> _globalCustomerList = new List<GameObject>(); 
    [SerializeField] List<GameObject> _listOfActiveCustomers = new List<GameObject>();
    [SerializeField] public List<CustomerWaypoint> _waypoints = new List<CustomerWaypoint>();


    [SerializeField] public float _customerMovementSpeed =1;

    [SerializeField] int _currentWaypointIndex;


    [SerializeField] private bool _isThereAnEmpytyWaypoint = false;
    

    private int _currentCustomerCount = 0;

    [SerializeField] public int _spawnCustomerTimer = 999;
    [SerializeField] private int _spawnCustomerTimerThreshold = 1000;

    //this says how close to the exit the customer is destroyed
    private const float DistanceToDestroyCustomer = 0.2f;

    public ProductManager _productManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnCustomerTimer = _spawnCustomerTimerThreshold / 3;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEmptyWaypoints();
        SpawnNextCustomer();

        foreach (GameObject customer in _listOfActiveCustomers)
        {
        
           if(!customer.GetComponent<Customer>()._isReadyToBeDestroyed)
            {
                if (_waypoints[0]._overlappingCustomer == customer &&
                    !_waypoints[1]._isOccupied && customer.GetComponent<Customer>()._hasSpawned == false)
                { 
                    customer.GetComponent<Customer>()._currentWaypointDestination = 1;
                    customer.GetComponent<Customer>()._hasSpawned = true;

                }

                if (_waypoints[1]._overlappingCustomer == customer && !_waypoints[2]._isOccupied && customer.GetComponent<Customer>()._hasSpawnedProducts == false)
                { 
                    customer.GetComponent<Customer>()._currentWaypointDestination = 2;
                    _productManager.UpdateCurrentProducts();
                    
                }
            }

           
            Vector3 direction = Vector3.Normalize(_waypoints[customer.GetComponent<Customer>()._currentWaypointDestination].transform.position - customer.transform.position) * _customerMovementSpeed * Time.deltaTime;
            if (Vector3.Distance(customer.transform.position, _waypoints[customer.GetComponent<Customer>()._currentWaypointDestination].transform.position) > 0.05f)
            {
                customer.transform.position += direction; //if the distance between the customer and the current waypoint is larger than a threshold, move the customer in that direction
                customer.GetComponent<Customer>()._isMoving = true;
            }
            else
            {
                customer.GetComponent<Customer>()._isMoving = false;
            }
           


            if( _productManager._currentCustomer!=null&& _productManager._currentCustomerProducts.Count == 0 )
            {
                MoveCustomerToExit(); //makes customer leave
            }

            RemoveLastCustomer();

           
        }

    }

    private void RemoveLastCustomer()
    {
        if (Vector3.Distance(_listOfActiveCustomers[0].transform.position, _waypoints[3].transform.position) < DistanceToDestroyCustomer)
        {

            Destroy(_listOfActiveCustomers[0]);
            _listOfActiveCustomers.RemoveAt(0);

            for (int i = 0; i < _waypoints.Count; i++)
            {
                // Shift items down by one
                _waypoints[i - 1] = _waypoints[i];


            }
        }
    }

    private void MoveCustomerToExit()
    {
        _listOfActiveCustomers[0].GetComponent<Customer>()._isReadyToBeDestroyed = true;
        _listOfActiveCustomers[0].GetComponent<Customer>()._currentWaypointDestination = 3;

        
    }

    private void SpawnNextCustomer()
    {
        if (_isThereAnEmpytyWaypoint)
        {
            _spawnCustomerTimer++;
            if (_spawnCustomerTimer >= _spawnCustomerTimerThreshold)
            {
                GameObject newCustomer = Instantiate(_globalCustomerList[_currentCustomerCount], _waypoints[0].transform.position, Quaternion.identity);
                _listOfActiveCustomers.Add(newCustomer);

                _spawnCustomerTimer = 0;
                _currentCustomerCount++;


            }

        }
    }

    private void CheckForEmptyWaypoints()
    {
        //foreach (CustomerWaypoint waypoint in _waypoints)
        //{
        //    if (waypoint._isOccupied == false)
        //    {
        //        _isThereAnEmpytyWaypoint = true;
        //        break;
        //    }
        //    else
        //    _isThereAnEmpytyWaypoint = false;
        //}
    

        _isThereAnEmpytyWaypoint = !_waypoints[0]._isOccupied;
    }

}
