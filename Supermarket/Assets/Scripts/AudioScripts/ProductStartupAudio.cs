using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProductStartupAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private void Awake()
    {
        audioSource.pitch = Random.Range(0.85f, 1.2f);
        audioSource.time = Random.Range(minTime, minTime);
        audioSource.PlayDelayed(Random.Range(0.01f, 0.05f));
    }

    
}
