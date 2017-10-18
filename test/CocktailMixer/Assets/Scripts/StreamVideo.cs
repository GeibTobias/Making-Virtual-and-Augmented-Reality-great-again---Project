using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {

	public RawImage image; 
	public VideoClip videoClip; 

	private VideoPlayer videoPlayer; 

	// Use this for initialization
	void Start () {
		Application.runInBackground = true; 
		StartCoroutine (playVideo()); 
	}

	void Update() {
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
