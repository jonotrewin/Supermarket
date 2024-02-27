using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Product : MonoBehaviour
{
    [SerializeField] public bool _hasBeenSpawned;
    [SerializeField] public bool _isReadyToBeScanned;
    [SerializeField] public bool _isScanned;
    [SerializeField] public bool _isPartOfCurrentBatch;
    [SerializeField] public bool _isCurrentlyInspected;

    public bool _isFacingDown = false;

    public GameObject _barcode;

    private ProductManager _productManager;

    [SerializeField] public string _identity;
    [SerializeField] public List<string> _identities;

    [SerializeField]private int _currentItemAppearance = 0;

    [SerializeField] private int _identityChangeCoefficient = 4; //changes how often new dialogue is played - whatever the coefficient is, -1, will be the amount of products that have the same identity text.

  
    private void Awake()
    {

        _productManager = GameObject.FindGameObjectWithTag("ProductManager").GetComponent<ProductManager>();

    }

    private void Start()
    {
        _productManager.productAppearanceTracker.Add(this.name);

        CountAppearances();


        _identity = GetCurrentIdentity();
    }

    private void CountAppearances()
    {
        foreach (string productName in _productManager.productAppearanceTracker)
        {
            if (this.name == productName)
            {
                _currentItemAppearance++;
            }
        }
    }

    private string GetCurrentIdentity()
    {
        if(_identities.Count <= 0)
        {
            return _identity;
        }
        if (_currentItemAppearance > _identities.Count)
        { /*return _identities[_identities.Count-1];*/ 
            System.Random random = new System.Random();
            int randomIdentityNumber = random.Next(0, Mathf.Max(0,_identities.Count-1));
            return _identities[randomIdentityNumber];
            
        
        }
        else {
            //return _identities[(int)(Math.Ceiling( 0.0 + _currentItemAppearance/_identityChangeCoefficient))]; //text changes every 3 lines
            return _identities[_currentItemAppearance -1];
         
        }
    }

    private void Update()
    {
        FindOutIfFacingDown();
        ProcessScanning();
    }

    private void ProcessScanning()
    {
        if (_isFacingDown)
        {
            _isReadyToBeScanned = true;           
        }
        else
        {
            _isReadyToBeScanned = false;
        }
    }



    private void FindOutIfFacingDown()
    {
        Ray barcodeRay = new Ray(_barcode.transform.position, _barcode.transform.forward);
        Physics.Raycast(barcodeRay, out RaycastHit hitInfo);
        Debug.DrawRay(_barcode.transform.position, _barcode.transform.forward);

        if (barcodeRay.direction.y < -0.95f)
        {
            _isFacingDown = true;
        }
        else
        {
            _isFacingDown = false;
        }
    }
}
