using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNametag : MonoBehaviour
{
    public NametagMenu _nametagMenu;
    // Start is called before the first frame update
    void Start()
    {
        string testString = ("Hello world!");
        _nametagMenu.AddIdentityString(testString);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("KeyPressed");
            string testString = ("Hello world!");
            _nametagMenu.AddIdentityString(testString);
            
        }
    }
}
