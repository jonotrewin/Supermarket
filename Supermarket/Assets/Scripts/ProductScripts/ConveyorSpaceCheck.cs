using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSpaceCheck : MonoBehaviour
{
    
    // Start is called before the first frame update

    public bool _isOccupied;

    public void FixedUpdate()
    {
        _isOccupied = false;
    }

    public void OnTriggerStay(Collider other)
    {
        _isOccupied = true;
        
    }

    public void OnTriggerExit(Collider other)
    {
        _isOccupied = false;
       
    }
}
