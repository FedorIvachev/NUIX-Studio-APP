using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWidget : ItemWidget
{

    public AudioSource _audioSource;

    public override void Start()
    {
        base.Start();
        InitWidget();
    }

    private void InitWidget()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        itemController.updateItem?.Invoke();
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

    public override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
