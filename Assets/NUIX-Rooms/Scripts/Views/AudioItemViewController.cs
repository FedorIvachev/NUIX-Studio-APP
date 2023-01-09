using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioItemViewController : ItemViewController
{

    public AudioClip[] audioClips;

    public AudioSource audioSource;

    public int audioClipIndex = 0;

    void Start()
    {
        audioSource.clip = audioClips[0];
        receiverMethods.Add(nameof(PlayNextClip));
        receiverMethods.Add(nameof(PlayPreviousClip));
        receiverMethods.Add(nameof(Play));
        receiverMethods.Add(nameof(Pause));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayNextClip()
    {
        audioClipIndex++;
        if (audioClipIndex >= audioClips.Length) audioClipIndex = 0;
        SetAudioClip(audioClipIndex);
        audioSource.Play();
    }

    public void PlayPreviousClip()
    {
        audioClipIndex--;
        if (audioClipIndex < 0) audioClipIndex = audioClips.Length - 1;
        SetAudioClip(audioClipIndex);
        audioSource.Play();
    }

    public void SetAudioClip(int audioClipIndex)
    {
        audioSource.clip = audioClips[audioClipIndex];
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }
}
