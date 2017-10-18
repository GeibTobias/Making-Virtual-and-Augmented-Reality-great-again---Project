using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour {

	public VideoPlayer player; 

	// Use this for initialization
	void Start () {

		player.playOnAwake = false; 
		player.frame = 100; 
		player.isLooping = true; 

		player.Play (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
