using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// USE THIS FOR SOUND VARIATIONS
[CreateAssetMenu(menuName = "Audio/AudioPatch")]
public class AudioPatch : ScriptableObject
{
    public AudioClip[] _audioClips;
    private List<AudioClip> _uniqueClips; //a list of remaining/unique clips

    [Range(0,1)] public float maxVolume = 1;
    [Range(0,1)] public float minVolume = 1;
    [Range(-3,3)] public float minPitch = 1;
    [Range(-3,3)] public float maxPitch = 1;

    private void Awake()
    {
        _uniqueClips = _audioClips.ToList(); //converting the given clips to a list, then putting them in _uniqueclips too
    }
    private void OnValidate() //makes it so we can mess around with the volume/pitch using sliders and gives us the possibility to play sounds with a random pitch and volume
    {
        if(minVolume > maxVolume)
        {
            minVolume = maxVolume;
        }
        if(minPitch > maxPitch)
        {
            minPitch = maxPitch;
        }
    }

    public void Play(AudioSource source)
    {
        source.clip = ReturnRandomClip(); //gets a random clip and loads it into the audio source
        //takes a random audio clip with a random pitch and volume within the user defined range.
        source.volume = Random.Range(minVolume, maxVolume);
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }

    public void PlayOneShot(AudioSource source) //Plays a oneshot sound (useful for playing multiple overlapping sounds from 1 gameobject)
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.PlayOneShot(ReturnRandomClip(), Random.Range(minVolume, maxVolume));
    }

    private AudioClip ReturnRandomClip()
    {

        if(_uniqueClips.Count > 0) //if there are still unique clips remaining within the uniqueclips variable
        {
            int randomIndex = Random.Range(0, _uniqueClips.Count); //makes a random index within the range of the audioclip lost 
            AudioClip randomClip = _uniqueClips[randomIndex]; //copies an audioclip using that random index
            _uniqueClips.RemoveAt(randomIndex); //removes the element at that index after copying it

            return randomClip; //returns this element
        }
        else //if the remaining unique elements run out
        {
            _uniqueClips = _audioClips.ToList(); //refresh the list of remaining elements and repeat
            int randomIndex = Random.Range(0, _uniqueClips.Count);
            AudioClip randomClip = _uniqueClips[randomIndex];
            _uniqueClips.RemoveAt(randomIndex);

            return randomClip;
        }
    }
}
