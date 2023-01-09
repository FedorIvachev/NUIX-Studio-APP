using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoItemViewController : ItemViewController
{

    public VideoClip[] videoClips;

    public VideoPlayer videoPlayer;
    public int videoClipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //videoPlayer.targetTexture.Release();
        videoPlayer.clip = videoClips[0];
        receiverMethods.Add(nameof(PlayNextClip));
        receiverMethods.Add(nameof(PlayPreviousClip));
        receiverMethods.Add(nameof(PlayVideo));
        receiverMethods.Add(nameof(PauseVideo));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNextClip()
    {
        videoClipIndex++;
        if (videoClipIndex >= videoClips.Length) videoClipIndex = 0;
        SetVideoClip(videoClipIndex);
        videoPlayer.Play();
    }

    public void PlayPreviousClip()
    {
        videoClipIndex--;
        if (videoClipIndex < 0) videoClipIndex = videoClips.Length - 1;
        SetVideoClip(videoClipIndex);
        videoPlayer.Play();
    }

    public void SetVideoClip(int videoClipIndex)
    {
        videoPlayer.clip = videoClips[videoClipIndex];
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }
}
