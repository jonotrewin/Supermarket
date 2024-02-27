using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGroup : MonoBehaviour

{
    // Start is called before the first frame update
   

    [SerializeField] bool _isComingFromSpeaker;
    [TextArea(3, 10)]
    public string[] _dialogueCollection;

    public string _name;

    public bool _isSkippable;

    public AudioClip[] _dialogueAudioClips;


}
