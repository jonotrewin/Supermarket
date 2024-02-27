using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScanningManager1 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject _currentlyScannedProduct;
    public GameObject _currentlyRotatedProduct;

    public Inspect _listOfPossibleProducts;

    public GameObject _inspector; 

    public NametagMenu _nametagMenu;

    [SerializeField] public TextMeshProUGUI _identityText;

    [SerializeField] public GameObject _ReadyToScanUI;

    [SerializeField] private GameObject _identityButton;
 
    private bool _shouldDisplayIdentityButton;



    void Start()
    {
        _inspector = GameObject.FindGameObjectWithTag("InspectionManager");
        _inspector.SetActive(false);
        _ReadyToScanUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

       
        FetchConveyorObject();



        FetchGUIObject();




        _identityText.text = _currentlyScannedProduct.GetComponent<Product>()._identity;



        //DisplayButtonForAddingIdentity();
        ScanProduct();


    }

    //private void DisplayButtonForAddingIdentity()
    //{
    //    for(int i = 0; i>_nametagMenu.listOfNames.Count; i++)
    //    {
    //        if (_nametagMenu.listOfNames[i].text == _identityText.text)
    //        {
    //            _shouldDisplayIdentityButton = false;
    //        }
    //        else
    //        {
    //            _shouldDisplayIdentityButton = true;
    //        }
    //    }

    //    if(_shouldDisplayIdentityButton == false || _inspector.activeSelf == false)
    //    {
    //        _identityButton.SetActive(false);
    //    }
    //    else
    //    {
    //        _identityButton.SetActive(true);
    //    }

    //}

    private void ScanProduct()
    {
        

       
        


        if (_currentlyRotatedProduct.GetComponent<Product>()._isFacingDown && _currentlyRotatedProduct.activeSelf == true)
        {
            _ReadyToScanUI.SetActive(true);
            

        }
        else
        {
            _ReadyToScanUI.SetActive(false);

        }
        
        if (_currentlyRotatedProduct.GetComponent<Product>()._isFacingDown
                     && Input.GetKeyDown(KeyCode.E))
        {
            
            _currentlyScannedProduct.GetComponent<Product>()._isScanned = true;
            _listOfPossibleProducts.DisableInspection();
            _inspector.SetActive(false);
            _ReadyToScanUI.SetActive(false);
        }


    }

    private void FetchConveyorObject()
    {
        if (_listOfPossibleProducts._isInspecting == false && Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, 50000f, LayerMask.GetMask("Products"));

            if (hit.collider == null) return;
            _currentlyScannedProduct = hit.collider.gameObject;


        }
    }

    private void FetchGUIObject()
    {
        foreach (GameObject product in _listOfPossibleProducts._inspectionObjects)
        {


            
            if (product.activeSelf)
            {
                
                _currentlyRotatedProduct = product;

            }
            
        }
      
    }

    public void AddIdentityFromProduct()
    {
        _nametagMenu.AddIdentityString(_identityText.text);
        

    }
}
