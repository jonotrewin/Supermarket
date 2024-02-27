using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProductAudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private Inspect _inspectScript;
    [SerializeField] private AudioPatchManager _audioPatchScript;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayProductAudio(int index)
    {
       AudioPatch currentAudioPatch = _audioPatchScript.GetAudioPatch(index);
       currentAudioPatch.Play(_audioSource);

    }
}
