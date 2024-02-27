using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestDialogueTrigger : MonoBehaviour
{
    public DialogueManager _dialogueManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _dialogueManager.TriggerDialogue((int)SceneTitles.StartingDialouge);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
