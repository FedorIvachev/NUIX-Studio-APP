using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Basic Item for operating with the attached Audio Source
    /// </summary>
    public class AudioItem : MonoBehaviour
    {
        GenericItem _audio;
        
        private AudioSource _audioSource;
        
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PauseAudioSource()
        {
            if (IsAudioSourcePlaying()) _audioSource.Pause();
        }

        public void PlayAudioSource()
        {
            if (!IsAudioSourcePlaying()) _audioSource.Play();
        }

        public bool IsAudioSourcePlaying()
        {
            return _audioSource.isPlaying;
        }

        /// <summary>
        /// Play/Pause Audio
        /// </summary>
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

        /// <summary>
        /// Change the clip to a new one
        /// </summary>
        /// <param name="newAudioClip"></param>
        public void ChangeAudioSource(AudioClip newAudioClip)
        {
            _audioSource.clip = newAudioClip;
        }

        /// <summary>
        /// Saving the state of the audio Source (the name)
        /// </summary>
        /// <param name="name"></param>
        void SerializeValue(string name = "")
        {
            _audio.name = name + "_audio";
            _audio.state = _audioSource.ToString();
            _audio.type = "Audio";
        }
    }
}
