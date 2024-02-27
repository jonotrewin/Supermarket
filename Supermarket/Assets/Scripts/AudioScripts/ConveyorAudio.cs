using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAudio : MonoBehaviour
{
    private AudioSource _conveyorAudioSource;


    private void Start()
    {
        _conveyorAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        _conveyorAudioSource.Stop();
    }

    private void OnTriggerExit(Collider other)
    {
        _conveyorAudioSource.Play();
    }
}
