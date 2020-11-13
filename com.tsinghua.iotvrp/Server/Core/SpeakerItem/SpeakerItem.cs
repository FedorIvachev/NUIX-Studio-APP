using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: refactor code to get; set 
namespace Tsinghua.HCI.IoTVRP
{
    public class SpeakerItem :MonoBehaviour
    {
        private AudioSource _audioSource;


        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
        }


        public void PauseAudioSource()
        {
            if (IsAudioSourcePlaying()) _audioSource.Pause();
        }

        public void PlayAudioSource()
        {
            if (!IsAudioSourcePlaying())_audioSource.Play();
        }

        public bool IsAudioSourcePlaying()
        {
            return _audioSource.isPlaying;
        }

        public void ToggleAudioSource()
        {
            if (IsAudioSourcePlaying()) PauseAudioSource();
            else PlayAudioSource();
        }

        public void MuteAudioSource()
        {
            _audioSource.mute = true;
        }

        public void UnMuteAudioSource()
        {
            _audioSource.mute = false;
        }

        public void DecreaseVolumeAudioSource(float value = 0.1f)
        {
            _audioSource.volume -= value;
        }

        public void IncreaseVolumeAudioSource(float value = 0.1f)
        {
            _audioSource.volume += value;
        }

        public void ChangeAudioSource(AudioClip newAudioClip)
        {
            _audioSource.clip = newAudioClip;
        }
    }
}