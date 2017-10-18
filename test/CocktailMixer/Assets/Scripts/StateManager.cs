using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public InstructionTextController instructions; 
	public BottlePositionController bottleController; 
	public StreamVideo videoStreamer; 

	private Receipe currentReceipe; 
	private int currentStep = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame(Receipe receipe)
	{
		this.currentReceipe = receipe; 
	}

	public void nextStep()
	{
		// check if next step
		// check, what bottles should be highlighted
		Step current = this.currentReceipe.getStep(currentStep); 
		currentStep++;

		if( videoStreamer != null ) 
		{
			//videoStreamer.setVideoClip (...);
		}
	}

	public void prevStep()
	{
	}
}
