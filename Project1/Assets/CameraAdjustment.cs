using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjustment : MonoBehaviour {

	public Camera m_OrthographicCamera;

	// Use this for initialization
	void Start () {
		m_OrthographicCamera = Camera.main;
		m_OrthographicCamera.enabled = true;

		//If the Camera exists in the inspector, enable orthographic mode and change the size
		if (m_OrthographicCamera) {
			//This enables the orthographic mode
			m_OrthographicCamera.orthographic = true;
			//Set the size of the viewing volume you'd like the orthographic Camera to pick up (5)
			m_OrthographicCamera.orthographicSize = 5.0f;

		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			m_OrthographicCamera.orthographicSize += 0.1f;
		}	
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			m_OrthographicCamera.orthographicSize -= 0.1f;
		}	

	}
}
