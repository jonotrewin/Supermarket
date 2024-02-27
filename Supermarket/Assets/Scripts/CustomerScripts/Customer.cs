using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Customer : MonoBehaviour
{
    public GameObject[] _productBatch;

    public bool _isReadyForNextDialogueLine;

    public bool _hasSpawned;
    public bool _hasSpawnedProducts;
    public bool _isCurrentCustomer;
    public bool _isNextCustomer;

    public bool _isMoving;
    private AudioSource _audioSource;
    private float _footstepTimer = 0.5f;
    private float _movementSpeed = 0.5f;
    [SerializeField] private AudioPatch _footsteps;

    public bool _hasDialogue;
    public bool _hasPlayedDialogue = false;
    [SerializeField]public SceneTitles sceneTitle; 
    [SerializeField] public bool _isReadyToBeDestroyed = false;

    public DialogueManager _dm;

    public int _currentWaypointDestination = 0;


   


    // Start is called before the first frame update
    void Start()
    {
        _dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteCustomerDialogue();

        if(_isMoving)
        {
            _footstepTimer -= Time.deltaTime;
            if(_footstepTimer <=0) 
            {
                _footsteps.Play(_audioSource);
                _footstepTimer = _movementSpeed;
            }
        }
        else
        {
            _footstepTimer = _movementSpeed;
        }
    }

    public void SwitchVisibility(bool _isOn)
    {
        GetComponent<Renderer>().enabled = _isOn;
    }

    public void ExecuteCustomerDialogue()
    {
        if(!_hasPlayedDialogue && _hasDialogue && _currentWaypointDestination ==2)
        {
            _dm.TriggerDialogue((int)sceneTitle);
           
            //Debug.Log("Playing " + (int)sceneTitle);
            _hasPlayedDialogue = true;
        }
    }

    
}
