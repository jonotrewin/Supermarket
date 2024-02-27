using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public List<String> productAppearanceTracker;

    public CustomerManager _cm;


    public GameObject _spawnPoint;

    public GameObject _inspectionManager;
    public Inspect _inspector;

    public List<GameObject> _allCurrentlySpawnedProducts = new List<GameObject>();
    public List<GameObject> _currentCustomerProducts = new List<GameObject>();

    public GameObject _conveyerFreeSpaceHitbox;
    public GameObject _hitboxCanConveyorRun;
    public GameObject _conveyorAudioCollision;

    public GameObject _scanWaypoint;
    public GameObject _endWaypoint;

    public float _distanceBetweenProducts;

    public float _productConveyerSpeed = 0.2f;

    [SerializeField]public bool _haveAllProductsBeenScanned = false;

   

    //[SerializeField] private List<GameObject> _batchToRemoveOnExit = new List<GameObject>();

    [SerializeField]public Customer _waitingCustomer;
    [SerializeField]public Customer _currentCustomer;
    [SerializeField] private Customer _previousCurrentCustomer;
    [SerializeField] private Customer _emptyCustomer;

    [SerializeField] private AudioSource _conveyorAudioSource;


    private void Start()
    {

        _previousCurrentCustomer = null;
        _currentCustomer = null;
        _waitingCustomer = null;
        _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        _scanWaypoint = GameObject.FindGameObjectWithTag("ScanPoint");
        _endWaypoint = GameObject.FindGameObjectWithTag("EndPoint");


    }

    void Update()
    {
        //Debug.Log(_waitingCustomer);

        //if(!_waitingCustomer == null)
        //{
        //    Debug.Log("Switch");
        //    _inspector.MakeInspectionAvailable();
        //}
        RemoveScannedProducts();
        _previousCurrentCustomer = _currentCustomer;
        _waitingCustomer = _cm._waypoints[1]._overlappingCustomerData;
        _currentCustomer = _cm._waypoints[2]._overlappingCustomerData;

        

        ActivateConveyor();

        

        CheckAndSpawnProducts();
        

        AddInspectorToProducts();


        DebugSkipCustomer(); //this is a debug command: press v to skip current customer
    }

    private void ActivateConveyor()
    {
        if (Input.GetKey(KeyCode.X))
        {
            ConveyerScroll();
        }
    }

    private void RemoveScannedProducts()
    {
        for(int i = 0; i < _currentCustomerProducts.Count; i++)
        {
            //Debug.Log("Checking");
            if (_currentCustomerProducts[i].gameObject.GetComponent<Product>()._isScanned)
            {
                //_currentCustomerProducts[i].transform.position += Vector3.Lerp(
                //   _currentCustomerProducts[i].transform.position, _scanWaypoint.transform.position,1000);

                GameObject productToDestroy = _currentCustomerProducts[i];
                _allCurrentlySpawnedProducts.Remove(_currentCustomerProducts[i]);
                //Destroy(_currentCustomerProducts[i]);

               

                _currentCustomerProducts.Remove(_currentCustomerProducts[i]);

                Destroy(productToDestroy);

                //Debug.Log("Found");

            }
        }    
    }

    private void CheckAndSpawnProducts()
    {
        if (!_conveyerFreeSpaceHitbox.GetComponent<ConveyorSpaceCheck>()._isOccupied)
        { AddCustomerProductsToProductListsAndSpawn(); }
        else
        { 
            ConveyerScroll();
          
            //CheckAndSpawnProducts();
        }
    }

    private void AddInspectorToProducts()
    {
        foreach(GameObject product in _allCurrentlySpawnedProducts)
        {
            SelectObject productSelectScript = product.GetComponent<SelectObject>();
            productSelectScript._inspectedItem = _inspector;
            productSelectScript._inspection = _inspectionManager;

        }
    }

    private void ConveyerScroll()
    {
        if (!_hitboxCanConveyorRun.GetComponent<ConveyorSpaceCheck>()._isOccupied)
        {
            //if(!_conveyorAudioSource.isPlaying)
            //{
            //    _conveyorAudioSource.Play();
            //}
            CheckAudioConveyorCollision();
            for (int i = 0; i < _allCurrentlySpawnedProducts.Count; i++)
            {
                _allCurrentlySpawnedProducts[i].gameObject.transform.position
                    -= _spawnPoint.transform.right * _productConveyerSpeed * Time.deltaTime;
            }
        }
        else
        {
            //_conveyorAudioSource.Stop();
        }    
    }

    private void CheckIfAllProductAreScanned()
    {
        foreach (GameObject product in _currentCustomerProducts)
        {
            if (!product.gameObject.GetComponent<Product>()._isScanned)
            {
                _haveAllProductsBeenScanned = false;
                break;
            }
           
        }
        _haveAllProductsBeenScanned = true;
    }

    private void AddCustomerProductsToProductListsAndSpawn()
    {
        if (_waitingCustomer != null && !_waitingCustomer._isNextCustomer )
        {

            for (int i = 0; i < _waitingCustomer._productBatch.Length; i++)
            {
                GameObject product = _waitingCustomer._productBatch[i];
                float spaceToMove = (_waitingCustomer._productBatch.Length + 1) - i; // this makes sure the products spawn in the correct order
                GameObject newProduct = Instantiate(product);
                newProduct.transform.position = _spawnPoint.transform.position - (_spawnPoint.transform.right.normalized * _distanceBetweenProducts * spaceToMove);
                newProduct.transform.position = newProduct.transform.position + new Vector3(0, 0.2f);
                _allCurrentlySpawnedProducts.Add(newProduct);

                newProduct.GetComponent<Product>()._hasBeenSpawned = true;




            }
            _waitingCustomer._isNextCustomer = true;
        }

        _conveyorAudioSource.Stop();




        //foreach(GameObject product in _allCurrentlySpawnedProducts)
        //{
        //    if(!product.GetComponent<Product>()._isPartOfCurrentBatch)
        //    {
        //        _currentCustomerProducts.Add(product);
        //        product.GetComponent<Product>()._isPartOfCurrentBatch = true;
        //    }
        //}

        //_currentCustomer._isCurrentCustomer = true;

    }

    public void UpdateCurrentProducts()
    {
        foreach (GameObject product in _allCurrentlySpawnedProducts)
        {
            if (!product.GetComponent<Product>()._isPartOfCurrentBatch)
            {
                _currentCustomerProducts.Add(product);
                product.GetComponent<Product>()._isPartOfCurrentBatch = true;
                //Debug.Log("Added Product");
            }
        }
    }

    public void DebugSkipCustomer()
    {
        if(Input.GetKey(KeyCode.V))
        {
            
            foreach(GameObject product in _currentCustomerProducts)
            {
                product.gameObject.GetComponent<Product>()._isScanned = true;
            }
            FindAnyObjectByType<DialoguePlayer>().EndDialogue();
        }
    }

    private void CheckAudioConveyorCollision()
    {
        if(!_conveyorAudioCollision.GetComponent<ConveyorSpaceCheck>()._isOccupied)
        {
            if(!_conveyorAudioSource.isPlaying)
            {
                _conveyorAudioSource.Play();
            }
        }
        else
        {
            _conveyorAudioSource.Stop();
        }
    }
}
