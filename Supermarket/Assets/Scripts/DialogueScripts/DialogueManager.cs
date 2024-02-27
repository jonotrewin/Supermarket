using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneTitles
{
    StartingDialouge,
    Unique_Regular_1,
    Unique_Regular_2,
    Unique_Regular_3,
    Unique_Regular_4,
    Generic_Regular_1,
    Generic_Regular_2,
    Generic_Regular_3,
    Generic_Regular_4,
    Generic_Regular_5,
    Generic_Regular_6,
    Generic_Regular_7,
    Weird_Cardboard,
    Weird_Laundry,
    Weird_Oil_1,
    Weird_Oil_2,
    Weird_Oil_3,
    Weird_Oil_4,
    Weird_Oil_5,
    Weird_Oil_6,
    Weird_Peanut,
    Generic_Strange_1,
    Generic_Strange_2,
    Generic_Strange_3,
    Generic_Strange_4,
    Generic_Strange_5,
    Announcement_1,
    Announcement_2,
    Announcement_3,
    Announcement_4,
    Announcement_5,
    Announcement_6,
    Announcement_7,
    Announcement_8,
    Final_Milk

}

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialoguePlayer _dialoguePlayer;

    public DialogueGroup[] _allDialogue;

    public bool _isDialoguePlaying;

    public Inspect _inspectedItem;


    void Start()
    {
        //this is how you trigger dialogue in game, calling the dialogue manager
        //TriggerDialogue((int)SceneTitles.firstDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        PauseGameWhenDialogueIsPlaying();
    }

    private void PauseGameWhenDialogueIsPlaying()
    {
        _inspectedItem._inspectorAvailable = !_isDialoguePlaying;
    }

    public void TriggerDialogue(int dialogueIndex)
    {
        _dialoguePlayer._currentDialogueCollection = _allDialogue[dialogueIndex];
        _dialoguePlayer._characterName = _allDialogue[dialogueIndex]._name;
        _dialoguePlayer.StartDialogue();
        //Debug.Log("TriggerDialogueExecuted");

    }
}
