using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {

	public RawImage image; 
	public VideoClip videoClip; 

	private Coroutine backgroundRoutine; 
	private VideoPlayer videoPlayer; 

	// Use this for initialization
	void Start () {
		Application.runInBackground = true; 
		backgroundRoutine = StartCoroutine (playVideo()); 
	}

	void Update() {
	}

	/**
	 * Method stops the the current video and starts the new given video.
	 */
	public void setVideoClip (VideoClip newClip) 
	{
		videoClip = newClip;
        setVideoClip(); 
    }

    public void setVideoClip()
    {
        stopVideo();
        backgroundRoutine = StartCoroutine(playVideo());
    }

    public void stopVideo()
    {
        if( backgroundRoutine != null )
        {
            videoPlayer.Stop(); 
            StopCoroutine(backgroundRoutine);
        }
    }
	
	IEnumerator playVideo()
	{
		videoPlayer = gameObject.AddComponent<VideoPlayer> ();

		videoPlayer.source = VideoSource.VideoClip; 
		videoPlayer.clip = videoClip;

		videoPlayer.playOnAwake = false; 
		videoPlayer.audioOutputMode = VideoAudioOutputMode.None; 

		videoPlayer.Prepare (); 

		WaitForSeconds waitTime = new WaitForSeconds (1);
		while (!videoPlayer.isPrepared) {
			yield return waitTime; 
			break; 
		}

		image.texture = videoPlayer.texture; 
		while (videoPlayer.isPlaying) {
			yield return null; 
		}
	}
}
