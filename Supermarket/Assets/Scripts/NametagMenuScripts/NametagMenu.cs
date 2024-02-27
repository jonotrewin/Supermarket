using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NametagMenu : MonoBehaviour
{


    public List<TextMeshProUGUI> listOfNames = new List<TextMeshProUGUI>();
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false );
        }

    }

    public void AddIdentityString(string identity)
    {
        //Debug.Log("method activated");
        for (int i = 0; i < listOfNames.Count; i++)
        {
            
            if(listOfNames[i].text == string.Empty)
            {
                //Debug.Log("name Added");
                listOfNames[i].text = identity;
                break;
            }
   
        }
    }
}
