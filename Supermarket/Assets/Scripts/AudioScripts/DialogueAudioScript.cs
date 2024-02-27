using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DialogueAudioScript : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _clipToPlay;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialogue()
    {
        _audioSource.clip = _clipToPlay;
        _audioSource.Play();
    }

    public void StopPlayback()
    {
        _audioSource?.Stop();
    }
}
