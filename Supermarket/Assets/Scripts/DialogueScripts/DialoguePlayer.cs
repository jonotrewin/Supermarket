using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialoguePlayer : MonoBehaviour
{
    public TextMeshProUGUI _textComponent;

    public DialogueGroup _currentDialogueCollection;

    [SerializeField] private float textSpeed;
    private int currentIndex;

    public string _characterName;

    //typing counter for sounds while dialogue is typing

    //int _typingCounter;
    //int _previousTypingCounter;
    //int _currentTypingCounter;

    //the line with the character name attached
    private string _currentCompiledDialogueLine;

    bool _isPlayingAnnouncement;
    public bool IsPlayingAnnouncement { get { return _isPlayingAnnouncement; } }
    CameraSwitch _camSwitch;
    Camera _mainCam;
    LightManager _lightMngr;
    DialogueManager _dialogueManager;
    [SerializeField] private SpeakerAudioScript _speakerAudioScript;
    [SerializeField] private DialogueAudioScript _dialogueAudioScript;
    // Start is called before the first frame update
    void Start()
    {
        _textComponent.text = string.Empty;
        _mainCam = FindObjectOfType<Camera>();
        _camSwitch = _mainCam.GetComponent<CameraSwitch>();
        _dialogueManager = FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
        _lightMngr= FindObjectOfType<LightManager>();
    }

    // Update is called once per frame
    private void OnEnable()
    {

        _dialogueManager._isDialoguePlaying = true;
        CheckTypeOfDialogue();
    }
    private void OnDisable()
    {
        _dialogueManager._isDialoguePlaying = false;
        CheckTypeOfDialogue();
    }
    public void SwitchCamera()
    {
        if (_currentDialogueCollection.gameObject.tag == "Announcement")
        {
            _camSwitch._isTimeToSwitch = !_camSwitch._isTimeToSwitch;
        }
    }

    void Update()
    {
        //CheckTypeOfDialogue();

        if (Input.GetMouseButtonDown(0))
        {
            if (_textComponent.text == _currentCompiledDialogueLine)
            {
                _dialogueAudioScript.StopPlayback();
                NextLine();

            }
            else
            {
                StopAllCoroutines();
                _textComponent.text = _currentCompiledDialogueLine;
                

            }
        }
    }

    private void CheckTypeOfDialogue()
    {
        if (_currentDialogueCollection.gameObject.tag == "Announcement" && _dialogueManager._isDialoguePlaying)
        {
            _isPlayingAnnouncement = true;
            _camSwitch._isTimeToSwitch = true;
            _camSwitch._isTimeToSwitchBack = false;
            Debug.Log(_isPlayingAnnouncement);
        }
        else if(_currentDialogueCollection.gameObject.tag == "Dialogue" || !_dialogueManager._isDialoguePlaying)
        {
            _isPlayingAnnouncement = false;
            _camSwitch._isTimeToSwitch = false;
            _camSwitch._isTimeToSwitchBack= true;
            Debug.Log(_isPlayingAnnouncement);
        }
        else if(_currentDialogueCollection.gameObject.tag == "FinalDialogue" && _dialogueManager._isDialoguePlaying)
        {
            _lightMngr._isSwitchingLighting= true;
        }
    }

    public void StartDialogue()
    {
        //Debug.Log("Starting dialogue");

        this.gameObject.SetActive(true);
        currentIndex = 0;
        _currentCompiledDialogueLine = _characterName + ": " + _currentDialogueCollection._dialogueCollection[currentIndex];
        if (_characterName.Length < 1)
        {
            _currentCompiledDialogueLine = _currentDialogueCollection._dialogueCollection[currentIndex];
        }
        //Debug.Log("Compiled dialogue line: " + _currentCompiledDialogueLine);
        if(_isPlayingAnnouncement)
        {
            _speakerAudioScript._clipToSwitchTo = _currentDialogueCollection._dialogueAudioClips[currentIndex];
            _speakerAudioScript.PlayAnnouncerChime();
        }
        else
        {
            _dialogueAudioScript._clipToPlay = _currentDialogueCollection._dialogueAudioClips[currentIndex];
            _dialogueAudioScript.PlayDialogue();
        }
        
        
        StartCoroutine(TypeLine());

    }

    void NextLine()
    {

        _textComponent.text = string.Empty;
        if (currentIndex < _currentDialogueCollection._dialogueCollection.Length - 1)
        {
            currentIndex++;
            if(_isPlayingAnnouncement)
            {
                _speakerAudioScript._clipToSwitchTo = _currentDialogueCollection._dialogueAudioClips[currentIndex];
                _speakerAudioScript.PlayFollowupDialogue();
                
                if (currentIndex == (_currentDialogueCollection._dialogueAudioClips.Length-1))
                {
                    _speakerAudioScript.SetIsPlaybackStopped(false);
                }
            }
            else
            {
                _dialogueAudioScript._clipToPlay = _currentDialogueCollection._dialogueAudioClips[currentIndex];
                _dialogueAudioScript.PlayDialogue();
            }
            _currentCompiledDialogueLine = new string(_characterName + ": " + _currentDialogueCollection._dialogueCollection[currentIndex]);
            if (_currentCompiledDialogueLine == ": " + _currentDialogueCollection._dialogueCollection[currentIndex])
            {
                _currentCompiledDialogueLine = _currentDialogueCollection._dialogueCollection[currentIndex];
            }
            StartCoroutine(TypeLine());

        }
        else
        {
            gameObject.SetActive(false);
            //GameObject.FindGameObjectWithTag("InspectedItem").GetComponent<Inspect>().MakeInspectionAvailable();
        }
    }

    IEnumerator TypeLine()
    {
        // type out each character 1 by 1
        yield return new WaitForSeconds(0.1f); // added delay so that the everything has time to initialize.

        foreach (char c in _currentCompiledDialogueLine.ToCharArray())
        {

            //_typingCounter++;
            _textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void EndDialogue()
    {
        _currentDialogueCollection = null;
        this.gameObject.active = false;

    }


}
