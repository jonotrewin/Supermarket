using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpeakerAudioScript : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _dayMusicClip;
    [SerializeField] private AudioClip _announcerChimeClip;
    [SerializeField] public AudioClip _clipToSwitchTo;
    [SerializeField] private List<AudioClip> _speakerDialogueClips = new List<AudioClip> ();
    private bool _isPlaybackStopped = false;
    private bool _timerStart = false;
    private float _timer;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        PlayMusic();
    }

    void Update()
    {
        if (_timerStart)
        {
            _timer += Time.deltaTime;
            if (_timer >= 3.5f)
            {
                PlayAnnouncerDialogue();
                _timer = 0;
                _timerStart = false;

            }
        }

        //HandleTestKeys();
        if (!_audioSource.isPlaying)
        {
            if(!_isPlaybackStopped)
            {
                PlayMusic();
            }
            
        }
        
        
        
    }

    public void PlayAnnouncerDialogue(/*int index*/)
    {
        //StopSpeakerPlayback();
        //_audioSource.loop = false;
        //_isPlaybackStopped = false;
        //_audioSource.clip = _speakerDialogueClips[index];
        //_audioSource.Play();
        _audioSource.loop = false;
        _audioSource.clip = /*_speakerDialogueClips[index]*/ _clipToSwitchTo;
        _audioSource.Play();

    }

    public void StopSpeakerPlayback()
    {
        _audioSource.Stop();
        _isPlaybackStopped = true;
    }

    public void PlayMusic()
    {
        _audioSource.clip = _dayMusicClip;
        _audioSource.loop = true;
        _isPlaybackStopped = false;
        _audioSource.Play();
    }

    public void PlayAnnouncerChime()
    {
        StopSpeakerPlayback();
        _audioSource.loop = false;
        //_isPlaybackStopped = false;
        _audioSource.clip = _announcerChimeClip;
        _audioSource.Play();
        _timerStart = true;
    }

    public void PlayFollowupDialogue()
    {
        StopSpeakerPlayback();
        _audioSource.loop = false;
        PlayAnnouncerDialogue();
        _isPlaybackStopped = true;
    }

    private void HandleTestKeys()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            //PlayAnnouncerDialogue(Random.Range(0, _speakerDialogueClips.Count));
            PlayAnnouncerChime();
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            PlayMusic();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            StopSpeakerPlayback();
        }
    }

    public void SetIsPlaybackStopped(bool playback)
    {
        _isPlaybackStopped= playback;
    }
}
