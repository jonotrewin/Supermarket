using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPatchManager : MonoBehaviour
{
    [SerializeField] public AudioPatch[] _audioPatches;
    private int _audioPatchIndex;

    public AudioPatch GetAudioPatch(int index)
    {
        return _audioPatches[index];
    }
}

