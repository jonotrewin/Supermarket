using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NametagManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public NametagMenu _nametagUI;
    public GameObject _rejectIdentityCanvas;

    public bool _isNametagFull;
    void Start()
    {
        _rejectIdentityCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OpenNametagOnClick();

        if (_nametagUI.listOfNames[_nametagUI.listOfNames.Count - 1].text != string.Empty)
        {
            _isNametagFull = true;
            OpenEndingScreen();

        }


    }

    private void OpenNametagOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, 50000f, LayerMask.GetMask("Products"));
            GameObject hitObject = hitInfo.collider.gameObject;
            Debug.Log(hitObject.gameObject.name);
            if (hitObject.tag == "Nametag")
            {
                
                _nametagUI.gameObject.SetActive(true);
            }

            //Debug.Log(hitObject);
        }
    }

    private void OpenEndingScreen()
    {
        _rejectIdentityCanvas.gameObject.SetActive(true);
        _nametagUI.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RejectIdentity()
    {
        for(int i = 0; i < _nametagUI.listOfNames.Count; i++)
        {
            _nametagUI.listOfNames[i].text = string.Empty;
        }
        Cursor.lockState = CursorLockMode.Locked;
        _rejectIdentityCanvas.gameObject.SetActive(false);
        _nametagUI.gameObject.SetActive(false);
    }
}
